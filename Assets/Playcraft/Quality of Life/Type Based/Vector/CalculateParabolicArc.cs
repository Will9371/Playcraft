using UnityEngine;

namespace Playcraft
{
    public class CalculateParabolicArc : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform source;
        [SerializeField] float range = 5f;
        [SerializeField] float gravity = 9.8f;
        [SerializeField] int resolution = 40;
        [SerializeField] float maxDrop = 5f;
        [SerializeField] Vector3ArrayEvent Output;
        [SerializeField] BoolEvent OnActivate;
        #pragma warning restore 0649
            
        float angle => -source.localEulerAngles.x; 
        float radianAngle => Mathf.Deg2Rad * angle;
        Vector3 forward => new Vector3(0f, source.eulerAngles.y, 0f); 
        
        bool active;
        public void SetActive(bool value) 
        {
            active = value;
            OnActivate.Invoke(value); 
        }

        public void Calculate()
        {
            if (!active) return;
            
            transform.eulerAngles = forward;
            Vector3[] arcArray = new Vector3[resolution + 1];

            float maxDistance = ((range * Mathf.Cos(radianAngle)) / gravity) * 
                                ((range * Mathf.Sin(radianAngle) + Mathf.Sqrt(Mathf.Pow(range * 
                                Mathf.Sin(radianAngle), 2) + (2 * gravity * maxDrop))));

            for (int j = 0; j <= resolution; j++)
            {
                float percent = (float)j / (float)resolution; 
                arcArray[j] = CalculateArcPoint(percent, maxDistance);
            }
            
            Output.Invoke(arcArray);
        }

        private Vector3 CalculateArcPoint(float t, float maxDistance)
        {
            float z = t * maxDistance;
            float y = z * Mathf.Tan(radianAngle) - ((gravity * z * z) / 
                     (2 * range * range * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

            return new Vector3(0f, y, z);
        }
    }
}
