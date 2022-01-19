using UnityEngine;
using Playcraft;

public class ThiefCursor : MonoBehaviour
{
    public RespondToCustomTag treasureIds;
    public RespondToCustomTag guardIds;
    
    public float cooldown = 0.5f;
    float lastCaughtTime;
    float lastStealTime;
    
    bool recentlyCaught => Time.time - lastCaughtTime < cooldown;
    bool recentlyStolen => Time.time - lastStealTime < cooldown;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (guardIds.Input(other))
            Caught();
        else if (treasureIds.Input(other))
            Steal();
    }
    
    void Caught()
    {
        if (recentlyCaught) return;
        lastCaughtTime = Time.time;
        Debug.Log("Caught!");
    }
    
    void Steal()
    {
        if (recentlyCaught || recentlyStolen) return;
        lastStealTime = Time.time;
        Invoke(nameof(Verdict), cooldown);
    }
    
    void Verdict()
    {
        if (recentlyCaught) return;
        Debug.Log("Steal!");
    }
}
