using UnityEngine;

public class RelayMessage : MonoBehaviour
{
    [SerializeField] IMessageEvent Output;

    public void Input(Collider value) { Input(value.GetComponent<IMessage>()); }
    public void Input(IMessage value) { if (value == null) return; Output.Invoke(value); }
}
