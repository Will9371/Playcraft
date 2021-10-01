using UnityEngine;

namespace Playcraft
{
    public class LerpAxisToIndexedAngle : MonoBehaviour
    {
        [SerializeField] GetFloatFromArray angles;
        [SerializeField] RotateAxis axis;
        
        [SerializeField] int startIndex = 0;
        [SerializeField] int endIndex = 1;

        public void Input(float percent)
        {
            var angle = Mathf.Lerp(angles.values[startIndex], angles.values[endIndex], percent);
            axis.SetAngle(angle);
        }

        void OnValidate() { axis.ValidateAngle(); }
        
        public void SetNewDestination(int newIndex)
        {
            startIndex = endIndex;
            endIndex = newIndex;
        }
    }
}
