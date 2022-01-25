using UnityEngine;

namespace ZMD
{
    public class LerpScaleArray : MonoBehaviour
    {
        [SerializeField] LerpScale[] scalars;
        
        public void Input(float value) 
        {
            foreach (var scalar in scalars)
                scalar.Input(value); 
        }
    }
}
