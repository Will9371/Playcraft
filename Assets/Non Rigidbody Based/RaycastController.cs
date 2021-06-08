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
        if(velocity.y != 0 )
        {
            VerticalCollisions(ref velocity, gravity);
        }

        Debug.DrawRay(transform.position, velocity * 30f, Color.green);

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
                    float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                    if (i == 0 && slopeAngle <= maxClimbAngle)
                    {
                        float distanceToSlopeStart = 0;
                        if(slopeAngle != collisions.slopeAngleOldX)
                        {
                            distanceToSlopeStart = hit.distance - skinWidth;
                            velocity -= forward * distanceToSlopeStart;
                        }
                        print("X slope is " + slopeAngle);
                        ClimbSlope(ref velocity, slopeAngle, true);
                        velocity += forward * distanceToSlopeStart;
                    }
                    else if ((!collisions.climbingSlopeX || slopeAngle > maxClimbAngle))
                    {
                        velocity = forward * (hit.distance - skinWidth);
                        rayLength = hit.distance;

                        if(collisions.climbingSlopeX)
                        {
                            velocity.y = Mathf.Tan(collisions.slopeAngleX * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                        }

                        collisions.front = true;
                    }
                }
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity, Vector3 gravity)
    {
        //...how we replacing velocity.x + velocity.z?
        //...how we handle slope climbing math?

        Vector3 horizontalMovement = velocity - Vector3.Dot(gravity, velocity) * gravity.normalized;

        float directionUp = -Mathf.Sign(Vector3.Dot(velocity, gravity));
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
                    velocity = transform.up * (hit.distance - skinWidth) * directionUp + horizontalMovement;
                    rayLength = hit.distance;

                    if(collisions.climbingSlopeX)
                    {
                        velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngleX * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                    }

                    collisions.below = directionUp == -1;
                    collisions.above = directionUp == 1;
                }
            }
        }
    }

    //Climbs in X or Z direction, toggled by xDir
    void ClimbSlope(ref Vector3 velocity, float slopeAngle, bool xDir)
    {
        float moveDistance = Mathf.Abs(xDir ? velocity.x : velocity.z);
        float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

        if (velocity.y <= climbVelocityY)
        {
            velocity.y = climbVelocityY;
            if (xDir)
            {
                velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                collisions.climbingSlopeX = true;
                collisions.slopeAngleX = slopeAngle;
            }
            else
            { 
                velocity.z = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.z);
                collisions.climbingSlopeZ = true;
                collisions.slopeAngleZ = slopeAngle;
            }
            collisions.below = true;
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

        public bool climbingSlopeX;
        public bool climbingSlopeZ;
        public bool climbingSlope
        {
            get { return climbingSlopeX || climbingSlopeZ; }
        }
        public float slopeAngleX, slopeAngleOldX;
        public float slopeAngleZ, slopeAngleOldZ;

        public void Reset()
        {
            above = front = right = false;
            below = back = left = false;
            climbingSlopeX = false;
            climbingSlopeZ = false;

            slopeAngleOldX = slopeAngleX;
            slopeAngleX = 0;
            slopeAngleOldZ = slopeAngleZ;
            slopeAngleZ = 0;
        }
    }

}
