using UnityEngine;

namespace ZMD.FlyCam
{
    public class FlyCamSettings : MonoBehaviour
    {
        [SerializeField] bool requireLeftMouseToTurn = true;
        [SerializeField] MultiMouseClickInputMono click;
        [SerializeField] GetMouseMovementMono drag;
            
        void Start() { SetLeftMouseRequiredToTurn(requireLeftMouseToTurn); }
        void OnValidate() { SetLeftMouseRequiredToTurn(requireLeftMouseToTurn); }

        public void SetLeftMouseRequiredToTurn(bool value)
        {
            click.enabled = requireLeftMouseToTurn;
            drag.enabled = !requireLeftMouseToTurn;            
        }
    }
}
