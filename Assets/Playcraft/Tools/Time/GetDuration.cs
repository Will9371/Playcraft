using UnityEngine;

public class GetDuration : MonoBehaviour
{
    float startTime;
    [SerializeField] FloatEvent Output;

    public void Begin()
    {
        startTime = Time.time;
    }
    
    public void End()
    {
        Output.Invoke(Time.time - startTime);
        Begin();
    }
}
