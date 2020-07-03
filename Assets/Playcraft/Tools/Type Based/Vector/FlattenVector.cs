using UnityEngine;

namespace Playcraft
{
    public class FlattenVector : MonoBehaviour
    {
        [SerializeField] Vector3Event Output = default;

        public void Input(Vector3 value)
        {
            value = new Vector3(value.x, 0f, value.z);
            Output.Invoke(value.normalized);
        }
    }
}
