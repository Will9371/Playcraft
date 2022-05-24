using UnityEngine;

namespace ZMD
{
    public class SendMessage : MonoBehaviour
    {
        [SerializeField] SO message;
        public void Input(Collider value) { Input(value.GetComponent<ISetSO>()); }
        public void Input(ISetSO value) { if (value == null) return; value.Message(message); }
    }
}
