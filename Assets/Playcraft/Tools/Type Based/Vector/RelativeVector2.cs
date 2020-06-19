using UnityEngine;

namespace Playcraft
{
    public class RelativeVector2 : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform source;
        [SerializeField] Transform target;
        [SerializeField] Vector2Event OnOutput;
        #pragma warning restore 0649

        public void Convert(Vector2 input)
        {
            if (input == Vector2.zero)
            {
                OnOutput.Invoke(Vector2.zero);
                return;
            }
        
            var sourceDirection = new Vector2(source.forward.x, source.forward.z);
            var targetDirection = new Vector2(target.forward.x, target.forward.z);
            var output = VectorMath.PerspectiveVector(input, sourceDirection, targetDirection);
            OnOutput.Invoke(output);
        }
    }
}
