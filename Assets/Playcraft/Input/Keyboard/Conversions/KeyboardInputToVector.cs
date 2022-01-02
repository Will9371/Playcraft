using UnityEngine;

namespace Playcraft
{
    public class KeyboardInputToVector : MonoBehaviour
    {
        [SerializeField] DirectionalKeybindings bindings;
        [SerializeField] Vector3Event output;
        
        void Update()
        {
            foreach (var binding in bindings.values)
                if (binding.input.IsActive())
                    output.Invoke(binding.value);
        }
    }
}
