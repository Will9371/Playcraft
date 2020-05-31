using UnityEngine;

namespace Playcraft
{
    public class FaceTarget : MonoBehaviour
    {
        Transform target;
        public void SetTarget(Transform target) { this.target = target; }
        public void ClearTarget() { target = null; }

        #pragma warning disable 0649
        [SerializeField] float minStartTurnAngle = 45f;
        [SerializeField] float minStopTurnAngle = 2f;
        [SerializeField] Vector3Event OnTurn;
        #pragma warning restore 0649
        
        Vector3 forward { get { return transform.forward; } }
        Vector3 targetVector { get { return !target ? Vector3.zero : target.position - transform.position; } }
        float angleToTarget { get { return StaticAxis.AngleAroundAxis(forward, targetVector, Vector3.up); } }
        float targetAngleAbs { get { return Mathf.Abs(angleToTarget); } }
        
        bool isTurning;
        

        void Update()
        {
            if (!target) return;
                
            isTurning = !isTurning && targetAngleAbs >= minStartTurnAngle || 
                         isTurning && targetAngleAbs >= minStopTurnAngle;  
                         
            if (isTurning)
            {
                var turnClockwise = angleToTarget > 0f;
                var axis = turnClockwise ? Vector3.up : -Vector3.up;
                OnTurn.Invoke(axis);
            }
        }
    }
}
