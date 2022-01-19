using UnityEngine;

public class PositionHistoryMono : MonoBehaviour
{
    public Transform indicator;
    public int recordCount = 10;
    
    PositionHistory process = new PositionHistory();
    
    void Start() { process.Initialize(transform.position, recordCount); }
    void FixedUpdate() { indicator.position = process.FixedUpdate(transform.position); }
}
