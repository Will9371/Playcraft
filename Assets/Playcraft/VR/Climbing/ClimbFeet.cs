using UnityEngine;

// Allows player to stand on ground without body interfering with climbing
namespace Playcraft
{
    namespace VR
    {
        public class ClimbFeet : MonoBehaviour
        {
            // Utilities
            LayerUtility layers = new LayerUtility();
            [SerializeField] LayerMask standLayer;

            // Dependencies
            [SerializeField] Transform head;
            [SerializeField] Transform rig;
            [SerializeField] Climb leftHand, rightHand;
            Collider footCollider;
            Collider headCollider;

            // Cached variables
            Ray headToFeet, priorToCurrent;
            RaycastHit hit;
            float height;
            bool isGrabbing;
            Vector3 priorFootPosition;  
            
            // Calculated
            Vector3 footPosition { get { return footCollider.transform.position; } }  

            void Awake()
            {
                footCollider = GetComponent<Collider>();
                headCollider = head.GetComponent<Collider>();
            }

            void Update()
            {
                this.isGrabbing = (leftHand.isGrabbing || rightHand.isGrabbing);
                footCollider.enabled = !isGrabbing;    //
                transform.localPosition = new Vector3(head.localPosition.x, 0f, head.localPosition.z);
                
                if (isGrabbing)
                    return;

                height = head.localPosition.y;

                if (height < .125f)
                    return;

                headToFeet = new Ray(head.position, Vector3.down);
                priorToCurrent = new Ray(priorFootPosition, footPosition);
                Debug.DrawRay(head.position, Vector3.down * height, Color.yellow);

                if (Physics.Raycast(headToFeet, out hit, height) && 
                    layers.IsIndexInBinary(hit.collider.gameObject.layer, standLayer) &&
                    hit.collider != footCollider && hit.collider != headCollider)
                    LiftUp(hit.point, hit.collider.name);
                //else if (Physics.Raycast(priorToCurrent, out hit, height) && 
                //    layers.IsIndexInBinary(hit.collider.gameObject.layer, standLayer) &&
                //    hit.collider != footCollider && hit.collider != headCollider)
                //    LiftUp(hit.point, hit.collider.name);
                
                priorFootPosition = footPosition;
            }
            
            private void LiftUp(Vector3 groundPoint, string groundName)
            {
                var liftDistance = Mathf.Max(0f, groundPoint.y - footPosition.y);
                //Debug.Log("LiftUp method reached " + liftDistance + ", " + groundName + ": " + groundPoint.y + 
                //    ", feet: " + footPosition.y + " delta: " + (groundPoint.y - footPosition.y));
                rig.Translate(Vector3.up * liftDistance);      
            }
        }
    }
}