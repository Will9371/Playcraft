using UnityEngine;

namespace Playcraft.FlyCam
{
    public class FlyCamSettings : MonoBehaviour
    {
        [SerializeField] bool requireLeftMouseToTurn = true;
        [SerializeField] GetMouseInput click;
        [SerializeField] GetMouseMovement drag;
            
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
