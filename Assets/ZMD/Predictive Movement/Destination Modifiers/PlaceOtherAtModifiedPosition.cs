using UnityEngine;

namespace ZMD
{
    public class PlaceOtherAtModifiedPosition : MonoBehaviour
    {
        [SerializeField] Transform other;
        [SerializeField] DestinationModifiers modifiers;
        void FixedUpdate() { other.position = modifiers.Tick(transform.position); }
    }
}