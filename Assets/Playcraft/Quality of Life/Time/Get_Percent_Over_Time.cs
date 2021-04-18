using UnityEngine;

public class Get_Percent_Over_Time
{
    float startTime;
    float elapsedTime;
    float duration;

    public void Begin(float duration)
    {
        this.duration = duration;
        startTime = Time.time;
        elapsedTime = 0f;
    }
    
    public float GetPercent()
    {
        elapsedTime = Time.time - startTime;
        return elapsedTime/duration;
    }
    
    float percent;
    
    public (float, bool) GetProgress()
    {
        percent = GetPercent();
        return (percent, percent >= 1f);
    }
}
