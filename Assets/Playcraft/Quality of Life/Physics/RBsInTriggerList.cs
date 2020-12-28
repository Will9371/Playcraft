using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class RBsInTriggerList : MonoBehaviour
    {
        public List<Rigidbody> rbs = new List<Rigidbody>();

        void OnCollisionEnter(Collision other) { OnTriggerEnter(other.collider); }
        void OnTriggerEnter(Collider other)
        {
            var otherRb = other.GetComponent<Rigidbody>();
            if (!otherRb || rbs.Contains(otherRb)) return;
            rbs.Add(otherRb);   
        }
        
        void OnCollisionExit(Collision other) { OnTriggerExit(other.collider); }
        void OnTriggerExit(Collider other)
        {
            var otherRb = other.GetComponent<Rigidbody>();
            if (!otherRb) return;
            if (rbs.Contains(otherRb)) rbs.Remove(otherRb);
        }
    }
}
