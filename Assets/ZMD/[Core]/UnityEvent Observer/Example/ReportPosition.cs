using UnityEngine;

namespace ZMD.Examples
{
    public class ReportPosition : MonoBehaviour
    {
        [SerializeField] EventSO value;
        public void Trigger() { value.onVector3?.Invoke(transform.position); }
    }
}