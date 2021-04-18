using System.Collections;
using Playcraft.Examples.SwordTrainer;
using UnityEngine;

public class TargetAdvanceRetreat : MonoBehaviour
{
    [SerializeField] SwordTarget controller;
    [SerializeField] Vector2 initialWaitRange;
    [SerializeField] Vector2 farDistanceWaitRange;
    [SerializeField] Vector2 closeDistanceWaitRange;
    
    void Start()
    {
        StartCoroutine(AdvanceRoutine());
    }
    
    IEnumerator AdvanceRoutine()
    {
        controller.SetRange(false);
        yield return new WaitForSeconds(GetRandom(initialWaitRange));
    
        while(true)
        {
            controller.SetRange(true);
            yield return new WaitForSeconds(GetRandom(closeDistanceWaitRange));
            controller.SetRange(false);
            yield return new WaitForSeconds(GetRandom(farDistanceWaitRange));
        }
    }
    
    float GetRandom(Vector2 range) { return Random.Range(range.x, range.y); }
}
