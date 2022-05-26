using UnityEngine;
using TMPro;
using ZMD;

public class DialogAIDisplay : Singleton<DialogAIDisplay>
{
    [SerializeField] TMP_Text narrative;
    
    public void Narrate(string value)
    {
        narrative.text = value;
    }
}
