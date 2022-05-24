using UnityEngine;

namespace ZMD.FlyCam
{
    public class FlyCamSettings : MonoBehaviour
    {
        [SerializeField] bool requireLeftMouseToTurn = true;
        [SerializeField] GetMouseInput click;
        [SerializeField] GetMouseMovementMono drag;
            
        void Start()
        {
            SetLeftMouseRequiredToTurn(requireLeftMouseToTurn);
        }
        
        public void SetLeftMouseRequiredToTurn(bool value)
        {
            click.enabled = requireLeftMouseToTurn;
            drag.enabled = !requireLeftMouseToTurn;            
        }
    }
}
