using UnityEngine;
using Playcraft;

public class ReturnToStartPoint : MonoBehaviour
{
    [SerializeField] EnterExitDistanceEvent rangeDetect;
    [SerializeField] Vector3Event OutputDirection;
    
    Vector3 targetDirection => (rangeDetect.center - rangeDetect.self.position).normalized;
            
    void Update() { OutputDirection.Invoke(targetDirection); }
    void OnDisable() { OutputDirection.Invoke(Vector3.zero); }    
}
