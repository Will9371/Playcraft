using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    //NOTE: This controller was made for non-rigidbody movement in a 3D space built from tiles sharing 
    //a uniform size and orientation. To that end, collision is handled by a boxCollider with an
    //orientation fixed to match those tiles 

    public LayerMask collisionMask;

    const float skinWidth = 0.15f;
    const float castBoxThickness = 0.01f;

    BoxcastOrigins castOrigins;

    [SerializeField] BoxCollider collider;

    public void Move(Vector3 velocity)
    {
        UpdateBoxcastOrigins();

        if(velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        if(velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }
        if(velocity.z != 0)
        {
            FrontBackCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = Mathf.Sign(velocity.x);
        float castLength = Mathf.Abs(velocity.x) + skinWidth;

        Vector3 castStart = (directionX == -1) ? castOrigins.leftCenter : castOrigins.rightCenter;
        RaycastHit[] hits = Physics.BoxCastAll(castStart, new Vector3(castBoxThickness, collider.size.y / 2, collider.size.z / 2), Vector3.right * directionX, transform.rotation, castLength, collisionMask);
        ExtDebug.DrawBoxCastBox(castStart, new Vector3(castBoxThickness, collider.size.y / 2, collider.size.z / 2), transform.rotation, Vector3.right * directionX, castLength, Color.red);

        float closestDistance = float.MaxValue;
        foreach(RaycastHit hit in hits)
        {
            if(hit.collider != this.collider && hit.distance - skinWidth < closestDistance) //ignore self collision
            {
                closestDistance = hit.distance - skinWidth;
                velocity.x = (hit.distance - skinWidth) * directionX; 
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float castLength = Mathf.Abs(velocity.y) + skinWidth;

        Vector3 castStart = (directionY == -1) ? castOrigins.downCenter : castOrigins.upCenter;
        RaycastHit[] hits = Physics.BoxCastAll(castStart, new Vector3(collider.size.x/2, castBoxThickness, collider.size.z / 2), Vector3.up * directionY, transform.rotation, castLength, collisionMask);
        ExtDebug.DrawBoxCastBox(castStart, new Vector3(collider.size.x / 2, castBoxThickness, collider.size.z / 2), transform.rotation, Vector3.up * directionY, castLength, Color.red);

        float closestDistance = float.MaxValue;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != this.collider && hit.distance - skinWidth < closestDistance) //ignore self collision
            {
                closestDistance = hit.distance - skinWidth;
                velocity.y = (hit.distance - skinWidth) * directionY;
            }
        }
    }

    void FrontBackCollisions(ref Vector3 velocity)
    {
        float directionZ = Mathf.Sign(velocity.z);
        float castLength = Mathf.Abs(velocity.z) + skinWidth;

        Vector3 castStart = (directionZ == -1) ? castOrigins.backCenter : castOrigins.frontCenter;
        RaycastHit[] hits = Physics.BoxCastAll(castStart, new Vector3(collider.size.x / 2, collider.size.y/2, castBoxThickness), Vector3.forward * directionZ, transform.rotation, castLength, collisionMask);
        ExtDebug.DrawBoxCastBox(castStart, new Vector3(collider.size.x / 2, collider.size.y/2, castBoxThickness), transform.rotation, Vector3.forward * directionZ, castLength, Color.red);

        float closestDistance = float.MaxValue;
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider != this.collider && hit.distance - skinWidth < closestDistance) //ignore self collision
            {
                closestDistance = hit.distance - skinWidth;
                velocity.z = (hit.distance - skinWidth) * directionZ;
            }
        }
    }

    void UpdateBoxcastOrigins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);
        castOrigins.leftCenter = new Vector3(bounds.min.x, bounds.center.y, bounds.center.z);
        castOrigins.rightCenter = new Vector3(bounds.max.x, bounds.center.y, bounds.center.z);
        castOrigins.downCenter = new Vector3(bounds.center.x, bounds.min.y, bounds.center.z);
        castOrigins.upCenter = new Vector3(bounds.center.x, bounds.max.y, bounds.center.z);
        castOrigins.backCenter = new Vector3(bounds.center.x, bounds.center.y, bounds.min.z);
        castOrigins.frontCenter = new Vector3(bounds.center.x, bounds.center.y, bounds.max.z);
    }

    struct BoxcastOrigins
    {
        public Vector3 downCenter, upCenter;
        public Vector3 frontCenter, backCenter;
        public Vector3 rightCenter, leftCenter;
    }
}
