using UnityEngine;

public class RespondToMessage : MonoBehaviour, IMessage
{
    [SerializeField] EventResponder responses = default;
    public void Message(SO value) { responses.GetResponse(value)?.Invoke(); }
}
