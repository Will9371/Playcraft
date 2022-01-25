using UnityEngine;

namespace ZMD
{
    public class TransformAccess : MonoBehaviour
    {
        public void SetActive(Transform other) { other.gameObject.SetActive(true); }
        public void SetInactive(Transform other) { other.gameObject.SetActive(false); }
    }
}
