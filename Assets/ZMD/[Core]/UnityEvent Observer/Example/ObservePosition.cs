using UnityEngine;

namespace ZMD.Examples
{
    public class ObservePosition : MonoBehaviour
    {
        [SerializeField] EventSO source;
        
        void OnEnable() { source.onVector3 += ReportPosition; }
        void OnDisable() { source.onVector3 -= ReportPosition; }
        
        void ReportPosition(Vector3 value) { Debug.Log($"{gameObject.name} {value}"); }
    }
}