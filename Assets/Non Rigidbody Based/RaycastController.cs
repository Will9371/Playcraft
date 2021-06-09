using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    //NOTE: This controller was made for non-rigidbody movement in a 3D space 
    //where the player can alter gravity.

    public LayerMask collisionMask;

    const float skinWidth = 0.15f;

    public int horizontalRayCount = 3;
    public int verticalRayCount = 4;
    public int frontBackRayCount = 3;

    public float maxClimbAngle = 50f;

    RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;

    #pragma warning disable 0649
    [SerializeField] new BoxCollider collider;
#pragma warning restore 0649

    private void Awake()
    {
        SetBoxcastDimensions();
    }

    public void ApplyCollisions(ref Vector3 velocity, Vector3 gravity)
    {
        UpdateBoxcastOrigins();
        collisions.Reset();

        Vector3 horizontalMovement = velocity - Vector3.Dot(velocity, gravity) * gravity.normalized;

        if (horizontalMovement.magnitude != 0)
        {
            HorizontalCollisions(ref velocity, horizontalMovement, gravity);
        }
        if(Vector3.Dot(velocity, -gravity) != 0 )
        {
            VerticalCollisions(ref velocity, gravity);
        }
    }

    void HorizontalCollisions(ref Vector3 velocity, Vector3 horizontalMovement, Vector3 gravity)
    {

        Vector3 forward = horizontalMovement.normalized;
        Vector3 up = -gravity.normalized;
        Vector3 right = Vector3.Cross(up, forward).normalized;

        float rayLength = horizontalMovement.magnitude + skinWidth;

        float latitudeSpacing = raycastOrigins.height / (verticalRayCount - 1);
        float longitudeSpacing = raycastOrigins.breadth / (horizontalRayCount - 1);
        for(int i = 0; i < verticalRayCount; i++)
        {
            for(int j = 0; j < horizontalRayCount; j++)
            {
                Vector3 rayOrigin = transform.position + forward * raycastOrigins.breadth / 2 - right * raycastOrigins.width / 2 - up * raycastOrigins.height / 2;
                rayOrigin += up * (i * latitudeSpacing) + right * (j * longitudeSpacing);
                RaycastHit hit;
                Debug.DrawRay(rayOrigin, forward * rayLength, Color.red);
                if(Physics.Raycast(rayOrigin, forward, out hit, rayLength, collisionMask))
                {
                    float slopeAngle = Vector3.Angle(hit.normal, transform.up);
                    if (i == 0 && slopeAngle <= maxClimbAngle)
                    {
                        float distanceToSlopeStart = 0;
                        if(slopeAngle != collisions.slopeAngleOld)
                        {
                            distanceToSlopeStart = hit.distance - skinWidth;
                            velocity -= forward * distanceToSlopeStart;
                        }
                        ClimbSlope(ref velocity, slopeAngle, ref horizontalMovement, gravity);
                        velocity += forward * distanceToSlopeStart;
                    }
                    else if ((!collisions.climbingSlope || slopeAngle > maxClimbAngle))
                    {
                        horizontalMovement = forward * (hit.distance - skinWidth);
                        velocity = horizontalMovement + Vector3.Dot(velocity, up) * up.normalized;
                        rayLength = hit.distance;

                        if(collisions.climbingSlope)
                        {
                            velocity = horizontalMovement + Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * horizontalMovement.magnitude * -gravity.normalized;
                        }

                        collisions.front = true;
                    }
                }
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity, Vector3 gravity)
    {
        Vector3 horizontalMovement = velocity - Vector3.Dot(gravity.normalized, velocity) * gravity.normalized;

        float directionUp = -Mathf.Sign(Vector3.Dot(velocity, gravity.normalized));
        float rayLength = Vector3.Dot(velocity, gravity.normalized) + skinWidth;

        float latitudeSpacing = raycastOrigins.width / (horizontalRayCount - 1);
        float longitudeSpacing = raycastOrigins.breadth / (frontBackRayCount - 1);

        for (int i = 0; i < horizontalRayCount; i++)
        {
            for (int j = 0; j < frontBackRayCount; j++)
            {
                Vector3 rayOrigin = (directionUp == -1) ? raycastOrigins.botBackLeft : raycastOrigins.topBackLeft;
                rayOrigin += transform.right * (i * latitudeSpacing + horizontalMovement.x + horizontalMovement.y + horizontalMovement.z) + transform.forward * (j * longitudeSpacing + horizontalMovement.x + horizontalMovement.y + horizontalMovement.z);
                RaycastHit hit;
                Debug.DrawRay(rayOrigin, transform.up * directionUp * rayLength, Color.red);
                if (Physics.Raycast(rayOrigin, transform.up * directionUp, out hit, rayLength, collisionMask))
                {
                    velocity = -gravity.normalized * (hit.distance - skinWidth) * directionUp; //Adds back horizontal movement later*
                    float newUp = -Mathf.Sign(Vector3.Dot(velocity, gravity.normalized)); //Recalculating directionUp in case it has just changed
                    rayLength = hit.distance;

                    if (collisions.climbingSlope) //*here
                    {
                        velocity += ((velocity.magnitude * newUp) / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(horizontalMovement.magnitude)) * horizontalMovement.normalized;
                    }
                    else//*or here
                    {
                        velocity += horizontalMovement;
                    }

                    collisions.below = directionUp == -1;
                    collisions.above = directionUp == 1;
                }
            }
        }
    }

    void ClimbSlope(ref Vector3 velocity, float slopeAngle, ref Vector3 horizontalMovement, Vector3 gravity)
    {
        float moveDistance = horizontalMovement.magnitude;
        float climbVelocityUp = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
        Vector3 upMovement = velocity - horizontalMovement;

        if (upMovement.magnitude <= climbVelocityUp)
        {
            upMovement = -gravity.normalized * climbVelocityUp;

            horizontalMovement = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * horizontalMovement.normalized;
            collisions.climbingSlope = true;
            collisions.slopeAngle = slopeAngle;
            collisions.below = true;

            velocity = upMovement + horizontalMovement;
        }
    }

    void SetBoxcastDimensions()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
        raycastOrigins.width = bounds.max.x - bounds.min.x;
        raycastOrigins.breadth = bounds.max.z - bounds.min.z;
        raycastOrigins.height = bounds.max.y - bounds.min.y;
    }

    void UpdateBoxcastOrigins()
    {
        float width = raycastOrigins.width;
        float breadth = raycastOrigins.breadth;
        float height = raycastOrigins.height;

        raycastOrigins.topFrontRight = transform.position + transform.forward * breadth / 2 + transform.right * width / 2 + transform.up * height / 2; 
        raycastOrigins.topFrontLeft = transform.position + transform.forward * breadth / 2 - transform.right * width / 2 + transform.up * height / 2;
        raycastOrigins.topBackRight = transform.position - transform.forward * breadth / 2 + transform.right * width / 2 + transform.up * height / 2;
        raycastOrigins.topBackLeft = transform.position - transform.forward * breadth / 2 - transform.right * width / 2 + transform.up * height / 2;
        raycastOrigins.botFrontRight = transform.position + transform.forward * breadth / 2 + transform.right * width / 2 - transform.up * height / 2;
        raycastOrigins.botFrontLeft = transform.position + transform.forward * breadth / 2 - transform.right * width / 2 - transform.up * height / 2;
        raycastOrigins.botBackRight = transform.position - transform.forward * breadth / 2 + transform.right * width / 2 - transform.up * height / 2;
        raycastOrigins.botBackLeft = transform.position - transform.forward * breadth / 2 - transform.right * width / 2 - transform.up * height / 2;
    }

    struct RaycastOrigins
    {
        public Vector3 topFrontRight, topFrontLeft, topBackRight, topBackLeft;
        public Vector3 botFrontRight, botFrontLeft, botBackRight, botBackLeft;
        public float width, breadth, height;
    }
    
    public struct CollisionInfo
    {
        public bool above, front, right;
        public bool below, back, left;

        public bool climbingSlope;
        public float slopeAngle, slopeAngleOld;

        public void Reset()
        {
            above = front = right = false;
            below = back = left = false;
            climbingSlope = false;

            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }

}
