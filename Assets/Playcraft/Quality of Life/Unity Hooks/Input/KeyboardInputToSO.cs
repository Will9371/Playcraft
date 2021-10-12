using UnityEngine;

namespace Playcraft
{
    public class KeyboardInputToSO : MonoBehaviour
    {
        [SerializeField] SOKeybindings bindings;
        [SerializeField] SOEvent output;
            
        void Update()
        {
            foreach (var binding in bindings.values)
                if (binding.input.IsActive())
                    output.Invoke(binding.value);
        }
    }
}
