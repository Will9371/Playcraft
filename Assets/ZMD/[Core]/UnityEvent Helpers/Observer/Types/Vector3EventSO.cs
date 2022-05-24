using UnityEngine;

namespace ZMD
{
    public class Vector3EventSO : MonoBehaviour
    {
        [SerializeField] EventSO id;
        [SerializeField] Vector3Event response;
        
        void OnEnable() { id.onVector3 += Trigger; }
        void OnDisable() { id.onVector3 -= Trigger; }
        
        void Trigger(Vector3 value) { response.Invoke(value); }
    }
}