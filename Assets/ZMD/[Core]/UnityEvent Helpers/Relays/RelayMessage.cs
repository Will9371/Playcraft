using UnityEngine;

namespace ZMD
{
    public class RelayMessage : MonoBehaviour
    {
        [SerializeField] IMessageEvent Output;
        public void Input(Collider value) { Input(value.GetComponent<ISetSO>()); }
        public void Input(ISetSO value) { if (value == null) return; Output.Invoke(value); }
    }
}
