using UnityEngine;

public class ContactEventBase : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other) { }

    protected virtual void OnTriggerExit(Collider other) { }

    private void OnCollisionEnter(Collision other)
    {
        OnTriggerEnter(other.collider);
    }

    private void OnCollisionExit(Collision other)
    {
        OnTriggerExit(other.collider);
    }
}
