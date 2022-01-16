using UnityEngine;

namespace Playcraft
{
    public class GetRandomDirection : MonoBehaviour
    {
        [SerializeField] Vector3Event Output;
        [SerializeField] bool flatten;

        public void Input()
        {
            var direction = Random.insideUnitSphere;
            if (flatten) direction.y = 0;
            direction = direction.normalized;
            Output.Invoke(direction);
        }
    }
}
