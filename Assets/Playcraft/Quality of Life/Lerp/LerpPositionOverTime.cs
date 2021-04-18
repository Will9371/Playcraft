using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LerpPositionOverTime : MonoBehaviour
{
    [SerializeField] Transform self;
    [SerializeField] bool useLocal = true;
    [SerializeField] UnityEvent OnComplete;
    
    Lerp_Position movement;
    
    void Awake()
    {
        movement = new Lerp_Position(self, useLocal);
    }
    
    public void Move(Vector3 destination, float duration)
    {            
        movement.SetDestination(destination);
        StartCoroutine(MoveRoutine(duration));
    }
    
    Get_Percent_Over_Time timer = new Get_Percent_Over_Time();
    
    IEnumerator MoveRoutine(float duration)
    {                
        timer.Begin(duration);
        (float percent, bool complete) progress = timer.GetProgress();
        
        while (!progress.complete)
        {
            movement.Input(progress.percent);
            yield return null;
            progress = timer.GetProgress();
        }
        
        OnComplete.Invoke();   
    }    
}
