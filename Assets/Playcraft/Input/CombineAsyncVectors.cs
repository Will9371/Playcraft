using UnityEngine;

namespace Playcraft
{
    public class CombineAsyncVectors : MonoBehaviour
    {        
        [SerializeField] Vector3Event OnMove;
                
        Vector3 input;
        
        public void AddMovement(Vector3SO vector) { AddMovement(vector.value); }  
        public void AddMovement(Vector3 vector) { input += vector; }
                
        void Update()
        {
            input = input.normalized;
            OnMove.Invoke(input);
            input = Vector3.zero; 
        }
    }
}
