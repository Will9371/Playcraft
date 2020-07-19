using Playcraft;
using UnityEngine;
using UnityEngine.UI;

public class SimpleDialogDisplay : MonoBehaviour
{
    [SerializeField] Text narrative;
    [SerializeField] DialogController controller;

    public void Input(DialogNode node)
    {
        narrative.text = node.narrative;
        controller.DisplayOptions();
    }
}
