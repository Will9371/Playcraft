using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    //NOTE: This controller was made for non-rigidbody movement in a 3D space built from tiles sharing 
    //a uniform size and orientation. To that end, collision is handled by a boxCollider with an
    //orientation fixed to match those tiles 

    public LayerMask collisionMask;

    const float skinWidth = 0.15f;

    public int horizontalRayCount = 3;
    public int verticalRayCount = 4;
    public int frontBackRayCount = 3;

    float maxClimbAngle = 50f;

    RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;

    #pragma warning disable 0649
    [SerializeField] new BoxCollider collider;
    #pragma warning restore 0649
    
    public void ApplyCollisions(ref Vector3 velocity)
    {
        UpdateBoxcastOrigins();
        collisions.Reset();

        if(velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        if(velocity.z != 0)
        {
            FrontBackCollisions(ref velocity);
        }
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }
    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        float latitudeSpacing = bounds.size.y / (verticalRayCount - 1);
        float longitudeSpacing = bounds.size.z / (horizontalRayCount - 1);
        for(int i = 0; i < verticalRayCount; i++)
        {
            for(int j = 0; j < horizontalRayCount; j++)
            {
                Vector3 rayOrigin = (directionX == -1) ? raycastOrigins.botBackLeft : raycastOrigins.botBackRight;
                rayOrigin += Vector3.up * (i * latitudeSpacing) + Vector3.forward * (j * longitudeSpacing);
                RaycastHit hit;
                Debug.DrawRay(rayOrigin, Vector3.right * directionX * rayLength, Color.red);
                if(Physics.Raycast(rayOrigin, Vector3.right * directionX, out hit, rayLength, collisionMask))
                {
                    float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                    if (i == 0 && slopeAngle <= maxClimbAngle)
                    {
                        float distanceToSlopeStart = 0;
                        if(slopeAngle != collisions.slopeAngleOldX)
                        {
                            distanceToSlopeStart = hit.distance - skinWidth;
                            velocity.x -= distanceToSlopeStart * directionX;
                        }
                        print("X slope is " + slopeAngle);
                        ClimbSlope(ref velocity, slopeAngle, true);
                        velocity.x += distanceToSlopeStart * directionX;
                    }
                    
                    if (i != 0 && (!collisions.climbingSlopeX || slopeAngle > maxClimbAngle))
                    {
                        velocity.x = (hit.distance - skinWidth) * directionX;
                        rayLength = hit.distance;

                        if(collisions.climbingSlopeX)
                        {
                            velocity.y = Mathf.Tan(collisions.slopeAngleX * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                        }

                        collisions.left = directionX == -1;
                        collisions.right = directionX == 1;
                    }
                }
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        float latitudeSpacing = bounds.size.x / (horizontalRayCount - 1);
        float longitudeSpacing = bounds.size.z / (frontBackRayCount - 1);
        for (int i = 0; i < horizontalRayCount; i++)
        {
            for (int j = 0; j < frontBackRayCount; j++)
            {
                Vector3 rayOrigin = (directionY == -1) ? raycastOrigins.botBackLeft : raycastOrigins.topBackLeft;
                rayOrigin += Vector3.right * (i * latitudeSpacing + velocity.x + velocity.z) + Vector3.forward * (j * longitudeSpacing + velocity.x + velocity.z);
                RaycastHit hit;
                Debug.DrawRay(rayOrigin, Vector3.up * directionY * rayLength, Color.red);
                if (Physics.Raycast(rayOrigin, Vector3.up * directionY, out hit, rayLength, collisionMask))
                {
                    velocity.y = (hit.distance - skinWidth) * directionY;
                    rayLength = hit.distance;

                    if(collisions.climbingSlopeX)
                    {
                        velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngleX * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                    }

                    collisions.below = directionY == -1;
                    collisions.above = directionY == 1;
                }
            }
        }
    }

    void FrontBackCollisions(ref Vector3 velocity)
    {
        float directionZ = Mathf.Sign(velocity.z);
        float rayLength = Mathf.Abs(velocity.z) + skinWidth;

        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        float latitudeSpacing = bounds.size.y / (verticalRayCount - 1);
        float longitudeSpacing = bounds.size.x / (horizontalRayCount - 1);
        for (int i = 0; i < verticalRayCount; i++)
        {
            for (int j = 0; j < horizontalRayCount; j++)
            {
                Vector3 rayOrigin = (directionZ == -1) ? raycastOrigins.botBackLeft : raycastOrigins.botFrontLeft;
                rayOrigin += Vector3.up * (i * latitudeSpacing) + Vector3.right * (j * longitudeSpacing);
                RaycastHit hit;
                Debug.DrawRay(rayOrigin, Vector3.forward * directionZ * rayLength, Color.red);
                if (Physics.Raycast(rayOrigin, Vector3.forward * directionZ, out hit, rayLength, collisionMask))
                {
                    float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
                    if (i == 0 && slopeAngle <= maxClimbAngle)
                    {
                        float distanceToSlopeStart = 0;
                        if(slopeAngle != collisions.slopeAngleOldZ)
                        {
                            distanceToSlopeStart = hit.distance - skinWidth;
                            velocity.z -= distanceToSlopeStart * directionZ;
                        }
                        print("Z slope is " + slopeAngle);
                        ClimbSlope(ref velocity, slopeAngle, false);
                        velocity.z += distanceToSlopeStart * directionZ;
                    }
                    
                    if (i != 0 && (!collisions.climbingSlopeZ || slopeAngle > maxClimbAngle))
                    {
                        velocity.z = (hit.distance - skinWidth) * directionZ;
                        rayLength = hit.distance;

                        if (collisions.climbingSlopeZ)
                        {
                            velocity.y = Mathf.Tan(collisions.slopeAngleZ * Mathf.Deg2Rad) * Mathf.Abs(velocity.z);
                        }

                        collisions.back = directionZ == -1;
                        collisions.back = directionZ == 1;
                    }
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

    void UpdateBoxcastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
        raycastOrigins.topFrontRight = new Vector3(bounds.max.x, bounds.max.y, bounds.max.z);
        raycastOrigins.topFrontLeft = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        raycastOrigins.topBackRight = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        raycastOrigins.topBackLeft = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        raycastOrigins.botFrontRight = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        raycastOrigins.botFrontLeft = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        raycastOrigins.botBackRight = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        raycastOrigins.botBackLeft = new Vector3(bounds.min.x, bounds.min.y, bounds.min.z);
    }

    struct RaycastOrigins
    {
        public Vector3 topFrontRight, topFrontLeft, topBackRight, topBackLeft;
        public Vector3 botFrontRight, botFrontLeft, botBackRight, botBackLeft;
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
