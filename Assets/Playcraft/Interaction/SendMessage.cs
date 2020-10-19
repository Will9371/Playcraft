using UnityEngine;

public class SendMessage : MonoBehaviour
{
    [SerializeField] TagSO message;
    public void Input(Collider value) { Input(value.GetComponent<IMessage>()); }
    public void Input(IMessage value) { if (value == null) return; value.Message(message); }
}
