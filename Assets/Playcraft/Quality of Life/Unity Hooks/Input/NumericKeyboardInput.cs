using UnityEngine;

namespace Playcraft
{
    public class NumericKeyboardInput : MonoBehaviour
    {
        [SerializeField] Keybindings bindings;
        [SerializeField] IntEvent output;
    
        void Update()
        {
            for (int i = 0; i < bindings.values.Length; i++)
                if (bindings.values[i].IsActive())
                    output.Invoke(i);
        }
    }
}
