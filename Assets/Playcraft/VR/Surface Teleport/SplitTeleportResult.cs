using UnityEngine;

public class SplitTeleportResult : MonoBehaviour
{
    [SerializeField] TrinaryEventData[] ioPaths;    
    
    public void Input(Trinary value)
    {
        foreach (var branch in ioPaths)
            if (branch.value == value)
                branch.OnActivate.Invoke();
    }
}

/*[Serializable]
public struct TeleportResultData
{
    public Trinary result;
    public UnityEvent Output;
}*/