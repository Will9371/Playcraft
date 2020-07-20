using UnityEngine;

namespace Playcraft
{
    public class GetNearestDirection : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Vector3Array directions;
        [SerializeField] Vector3Event Output;
        #pragma warning restore 0649

        public void Input(Vector3 value)
        {
            var dots = new float[directions.values.Length];
            float highestValue = -1f;
            int highestIndex = 0;
            
            for (int i = 0; i < dots.Length; i++)
            {
                var dot = Vector3.Dot(value, directions.values[i].normalized);
                dots[i] = dot;
                
                if (dot > highestValue)
                {
                    highestValue = dot;
                    highestIndex = i;
                }
            }
            
            Output.Invoke(directions.values[highestIndex].normalized);
        }
    }
}
