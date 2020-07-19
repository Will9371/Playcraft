using Playcraft;
using UnityEngine;

public class GetNarrativeFromDialogNode : MonoBehaviour
{
    [SerializeField] StringEvent RelayString;
    
    public void Input(DialogNode node)
    {
        RelayString.Invoke(node.narrative);
    }
}
