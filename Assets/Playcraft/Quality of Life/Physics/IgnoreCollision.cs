using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField] Collider[] ownColliders;
    [SerializeField] Collider[] otherColliders;

    void Start()
    {
        foreach (var self in ownColliders)
            foreach (var other in otherColliders)
                Physics.IgnoreCollision(self, other);
    }
}
