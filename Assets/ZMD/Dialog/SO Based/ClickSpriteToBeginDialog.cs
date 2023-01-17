using UnityEngine;

namespace ZMD.Dialog
{
    public class ClickSpriteToBeginDialog : MonoBehaviour
    {
        DialogController controller => NarrativeHub.instance.dialog.process;
        [SerializeField] DialogNode node;
        void OnMouseDown() => controller.Begin(node);
    }
}
