// Source: https://answers.unity.com/questions/254130/how-do-i-rotate-an-object-towards-a-vector3-point.html
// Credit: asafsitner

using UnityEngine;

namespace Playcraft
{
    public class SlerpToLookAt: MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float rotationSpeed;
        [SerializeField] FloatEvent Angle;
     
        Quaternion lookRotation;
        Vector3 direction;
         
        void Update()
        {
            // Find the vector pointing from our position to the target
            direction = (target.position - transform.position).normalized;
     
            // Create the rotation we need to be in to look at the target
            lookRotation = Quaternion.LookRotation(direction);
             
            // Rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            
            // Broadcast the remaining angle to target
            Angle.Invoke(Quaternion.Angle(transform.rotation, lookRotation));
        }
    }
}
