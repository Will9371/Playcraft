using UnityEngine;

namespace Playcraft
{
    public class NumericKeyboardInputMono : MonoBehaviour
    {
        [SerializeField] NumericKeyboardInput process;
        [SerializeField] IntEvent output;
        
        void Update() 
        {
            process.Update();
            foreach (var value in process.activeValues)
                output.Invoke(value);
        }
    }
}
