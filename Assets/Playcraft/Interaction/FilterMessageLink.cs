using Playcraft;
using UnityEngine;

public class FilterMessageLink : MonoBehaviour
{
    [SerializeField] MessageLinkEvent Output;
    
    public void Input(Collider value)
    {
        var link = value.GetComponent<MessageLink>();
        if (link) Output.Invoke(link);
    }
}
