using UnityEngine;

namespace Playcraft.VR
{
    public class JumpTeleport : MonoBehaviour
    {
        [SerializeField] Transform[] moveOnJump;
        [SerializeField] GenerateRadialPlacement jumpPoints;
        [SerializeField] GameObject centerPoint;
        
        void Start()
        {
            SetActive(true);
        }
        
        public void TouchDirection(Transform value)
        {
            Teleport(value);
            SetActive(false);
        }
        
        public void TouchCenter() { SetActive(true); }
        
        void Teleport(Transform location) 
        {
            var step = location.localPosition;
            foreach (var item in moveOnJump)
                item.Translate(step);
        }
        
        void SetActive(bool value)
        {
            jumpPoints.SetActive(value);
            centerPoint.SetActive(!value);
        }
    }
}