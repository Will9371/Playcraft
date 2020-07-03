using UnityEngine;

namespace Playcraft
{
    public class GetRandomDirection : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Vector3Event Output;
        [SerializeField] bool flatten;
        #pragma warning restore 0649

        public void Input()
        {
            var direction = Random.insideUnitSphere;
            if (flatten) direction.y = 0;
            direction = direction.normalized;
            Output.Invoke(direction);
        }
    }
}
