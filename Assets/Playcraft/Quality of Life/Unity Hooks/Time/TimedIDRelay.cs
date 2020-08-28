using UnityEngine;

public class TimedIDRelay : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] float time;
    [SerializeField] TagEvent OnEnd;
    #pragma warning restore 0649
    
    TagSO id;
    
    public void Begin(TagSO value) 
    {
        id = value;
        Invoke(nameof(End), time); 
    }

    private void End() { OnEnd.Invoke(id); }
        
    public void Cancel() { CancelInvoke(nameof(End)); }
        
    public void SetTime(float value) { time = value; }
}
