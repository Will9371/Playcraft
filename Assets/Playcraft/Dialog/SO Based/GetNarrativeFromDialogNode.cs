using Playcraft;
using UnityEngine;

namespace Playcraft.Dialog
{
    public class GetNarrativeFromDialogNode : MonoBehaviour
    {
        [SerializeField] StringEvent RelayString = default;
        
        public void Input(DialogNode node)
        {
            RelayString.Invoke(node.narrative);
        }
    }
}
