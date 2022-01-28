using UnityEngine;

namespace ZMD
{
    public class ValidateChildren : MonoBehaviour
    {
        [SerializeField] TransformArrayEvent output;
        [SerializeField] bool trigger;
        
        void OnValidate() 
        {
            if (trigger)
            { 
                trigger = false;
                output.Invoke(StaticHelpers.GetChildren(transform)); 
            }
        }
    }
}
