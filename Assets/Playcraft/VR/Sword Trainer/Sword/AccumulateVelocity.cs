using UnityEngine;

public class AccumulateVelocity : MonoBehaviour
{
    [SerializeField] int runningAverageLength;
    [SerializeField] FloatEvent OutputAverageMagnitude;
    
    Vector3[] positions;
    Vector3[] deltas;
    int overwriteIndex;
    
    Vector3 averageDelta;
    float averageMagnitude => averageDelta.magnitude / Time.fixedDeltaTime;
    
    void Start() { Initialize(); }
    
    int priorRunningAverageLength;
    
    void OnValidate()
    {
        if (priorRunningAverageLength != runningAverageLength)
            Initialize();
    }
    
    void Initialize()
    {
        positions = new Vector3[runningAverageLength];
        deltas = new Vector3[runningAverageLength];
        overwriteIndex = 0;
        priorRunningAverageLength = runningAverageLength;     
    }

    void FixedUpdate()
    {
        RecordPosition();
        SetAverageVector();
    }
    
    void RecordPosition()
    {
        deltas[overwriteIndex] = transform.position - positions[overwriteIndex];
        positions[overwriteIndex] = transform.position;
        
        overwriteIndex++;
        if (overwriteIndex >= runningAverageLength)
            overwriteIndex = 0;        
    }
        
    void SetAverageVector()
    {
        averageDelta = Vector3.zero;
        
        foreach (var delta in deltas)
            averageDelta += delta;
            
        averageDelta /= runningAverageLength;
        OutputAverageMagnitude.Invoke(averageMagnitude);
    }
}
