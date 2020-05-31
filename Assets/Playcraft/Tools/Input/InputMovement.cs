using UnityEngine;

namespace Playcraft
{
    public class InputMovement : MonoBehaviour
    {        
        #pragma warning disable 0649
        [SerializeField] Vector3Event OnMove;
        #pragma warning restore 0649
                
        Vector3 moveInput;
        
        public void AddMovement(Vector3SO direction) { AddMovement(direction.value); }  
        public void AddMovement(Vector3 direction) { moveInput += direction; }
        
        private void Update()
        {
            moveInput = moveInput.normalized;
            OnMove.Invoke(moveInput);
            moveInput = Vector3.zero; 
        }
    }
}
