using UnityEngine;

// Input: Game Engine
// Process: continuous check for mouse button clicks
// Output: event triggers per type
namespace Playcraft
{
    public class GetMouseInput : MonoBehaviour
    {
        [SerializeField] MouseClickInput[] clickInput = default;

        private void Update()
        {            
            foreach (var input in clickInput)
                input.Update();                
        }
    }
}
