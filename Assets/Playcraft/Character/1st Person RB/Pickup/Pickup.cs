using UnityEngine;

namespace Playcraft
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] Rigidbody rb;
        Transform objectToFollow;
        
        public void Input(GameObject source, bool isPickingUp)
        {
            if (isPickingUp) GetPickedUp(source.transform);
            else GetDropped();
        }
        
        void GetPickedUp(Transform pickerUpper)
        {
            objectToFollow = pickerUpper;
            rb.useGravity = false;
        }
        
        void GetDropped()
        {
            objectToFollow = null;
            rb.useGravity = true;
        }
        
        private void Update()
        {
            if (objectToFollow)
                transform.SetPositionAndRotation(objectToFollow.position, objectToFollow.rotation);
        }
    }
}
