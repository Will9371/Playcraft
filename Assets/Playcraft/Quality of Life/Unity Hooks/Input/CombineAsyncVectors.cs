using UnityEngine;

namespace Playcraft
{
    public class CombineAsyncVectors : MonoBehaviour
    {        
        #pragma warning disable 0649
        [SerializeField] Vector3Event OnMove;
        #pragma warning restore 0649
                
        Vector3 input;
        
        public void AddMovement(Vector3SO direction) { AddMovement(direction.value); }  
        public void AddMovement(Vector3 direction) { input += direction; }
                
        private void Update()
        {
            input = input.normalized;
            OnMove.Invoke(input);
            input = Vector3.zero; 
        }
        
        /*bool locked;
        Vector3 lockedDirection;
        public void SetDirectionLock(bool value) 
        { 
            locked = value;
            lockedDirection = input.normalized;
        }
        
        public Vector3 direction => locked ? lockedDirection : input;*/
    }
}
