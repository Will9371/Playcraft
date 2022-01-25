using UnityEngine;

namespace ZMD
{
    public class GetChildFromKeyboardInputMono : MonoBehaviour
    {
        [SerializeField] GetChildFromKeyboardInput process;
        [SerializeField] TransformEvent output;
        [SerializeField] bool outputResultIfNull;
        
        Transform result;
        
        void Update() 
        { 
            result = process.GetFirstOrNull();
            
            if (result || outputResultIfNull) 
                output.Invoke(result);
        }
    }
}
