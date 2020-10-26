using UnityEngine;

namespace Playcraft
{
    public class SendMessage : MonoBehaviour
    {
        [SerializeField] SO message = default;
        public void Input(Collider value) { Input(value.GetComponent<IMessage>()); }
        public void Input(IMessage value) { if (value == null) return; value.Message(message); }
    }
}
