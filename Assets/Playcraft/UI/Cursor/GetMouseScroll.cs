using UnityEngine;

// Input: Game Engine
// Process: continuous check for mouse scroll
// Output: scroll value this frame
namespace Playcraft
{
    public class GetMouseScroll : MonoBehaviour
    {
        [SerializeField] float scrollSensitivity = 1f;
        [SerializeField] FloatEvent OnMouseScroll = default;
        
        void Update()
        {
            if (Input.mouseScrollDelta.y != 0)
                OnMouseScroll.Invoke(Input.mouseScrollDelta.y * scrollSensitivity);
        }
    }
}
