using UnityEngine;

namespace ZMD
{
    public class GameObjectAccess : MonoBehaviour
    {
        public void SetActive(GameObject other) { other.SetActive(true); }
        public void SetInactive(GameObject other) { other.SetActive(false); }
    }
}
