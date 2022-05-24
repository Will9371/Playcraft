using System;
using UnityEngine;

namespace ZMD
{
    public class ObservedCollider : MonoBehaviour
    {
        [ReadOnly] public Collider self;
        void OnValidate() { self = GetComponent<Collider>(); }
        
        public Action<Collider, Collider> onTriggerEnter;
        public Action<Collider, Collider> onTriggerExit;
        public Action<Collider, Collider> onTriggerStay;

        void OnTriggerEnter(Collider other) { onTriggerEnter?.Invoke(self, other); }
        void OnTriggerExit(Collider other) { onTriggerExit?.Invoke(self, other); }
        void OnTriggerStay(Collider other) { onTriggerStay?.Invoke(self, other); }
        
        public Action<Collider, Collision> onCollisionEnter;
        public Action<Collider, Collision> onCollisionExit;
        public Action<Collider, Collision> onCollisionStay;

        void OnCollisionEnter(Collision other) { onCollisionEnter?.Invoke(self, other); }
        void OnCollisionExit(Collision other) { onCollisionExit?.Invoke(self, other); }
        void OnCollisionStay(Collision other) { onCollisionStay?.Invoke(self, other); }
    }
}
