using UnityEngine;

public class RespondToMessage : MonoBehaviour, IMessage
{
    [SerializeField] EventResponder responses;
    public void Message(TagSO value) { responses.GetResponse(value)?.Invoke(); }
}
