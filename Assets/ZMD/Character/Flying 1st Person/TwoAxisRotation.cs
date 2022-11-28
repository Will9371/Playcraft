using UnityEngine;

namespace ZMD
{
    public class TwoAxisRotation : MonoBehaviour
    {
        [SerializeField] bool invertX;
        [SerializeField] bool invertY = true;
        
        [SerializeField] bool clampX;
        [SerializeField] Vector2 xRange;
        [SerializeField] bool clampY;
        [SerializeField] Vector2 yRange;
        
        private float x, y;

        public void Rotate(Vector2 value)
        {
            x += value.y * (invertY ? -1f : 1f);
            y += value.x * (invertX ? -1f : 1f);
            
            if (clampX) x = RangeMath.ApplyMinMax(x, xRange);
            if (clampY) y = RangeMath.ApplyMinMax(y, yRange);
            
            transform.eulerAngles = new Vector3(x, y, 0);
        }
    }
}
