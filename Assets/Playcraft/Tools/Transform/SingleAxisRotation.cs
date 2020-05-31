using UnityEngine;

namespace Playcraft
{
    public class SingleAxisRotation : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Axis axis;
        [SerializeField] bool invert;
        [SerializeField] bool clamp;
        [SerializeField] Vector2 range;
        #pragma warning restore 0649
        
        private float value;

        public void Rotate(float magnitude)
        {
            value += magnitude * (invert ? -1f : 1f);
            if (clamp) value = RangeMath.ApplyMinMax(value, range);
            
            switch (axis)
            {
                case Axis.X: transform.eulerAngles = new Vector3(value, transform.eulerAngles.y, transform.eulerAngles.z); break;
                case Axis.Y: transform.eulerAngles = new Vector3(transform.eulerAngles.x, value, transform.eulerAngles.z); break;
                case Axis.Z: transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, value); break;
            }
        }
    }
}
