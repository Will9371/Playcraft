using UnityEngine;

namespace Playcraft.Navigation
{
    public class OffsetDirection : MonoBehaviour
    {
        [SerializeField] Vector3Event Output = default;
        
        Vector3 direction;
        public void SetDirection(Vector3 value) { direction = value; }
        
        TurnType turnType;
        public void SetTurnType(TurnType value) { turnType = value; }
        
        
        private void Update()
        {
            var adjustedDirection = direction;
            
            switch (turnType)
            {
                case TurnType.Left: adjustedDirection = (direction - transform.right).normalized; break;
                case TurnType.Right: adjustedDirection = (direction + transform.right).normalized; break;
            }
            
            Output.Invoke(adjustedDirection);
        }
    }
}
