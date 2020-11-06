using UnityEngine;

namespace Playcraft.FlyCam
{
    public class FlyCamSettings : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] bool requireLeftMouseToTurn = true;
        [SerializeField] GetMouseInput click;
        [SerializeField] GetMouseMovement drag;
        #pragma warning restore 0649
            
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
