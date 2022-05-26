using UnityEngine;

public class DialogAINPC : MonoBehaviour
{
    DialogAIDisplay display => DialogAIDisplay.instance;

    public DialogAINPCInfo info;
    public DialogAINPCData data;
    
    void Start()
    {
        Reset();
    }
    
    public void Select(int optionIndex)
    {
        var state = data.GetState();
        var text = state ? state.statement : "...";
        display.Narrate(text);
    }
    
    public void Reset()
    {
        data = info.Instantiate();
    }
}
