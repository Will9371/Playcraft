using UnityEngine;

namespace ZMD
{
    public class CombineAsyncVectorsMono : MonoBehaviour
    {
        CombineAsyncVectors process = new CombineAsyncVectors(); 
               
        [SerializeField] Vector3Event OnMove;
        
        public void AddMovement(Vector3SO vector) { process.AddMovement(vector.value); }  
        public void AddMovement(Vector3 vector) { process.AddMovement(vector); }
        
        void Update() { OnMove.Invoke(process.Update()); }
    }
    
    public class CombineAsyncVectors
    {
        Vector3 input;
        Vector3 output;
        
        public void AddMovement(Vector3 vector) { input += vector; }
                
        public Vector3 Update()
        {
            output = input.normalized;
            input = Vector3.zero;
            return output;
        }        
    }
}
