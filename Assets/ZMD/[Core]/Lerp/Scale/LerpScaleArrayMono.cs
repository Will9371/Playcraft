using System;
using UnityEngine;

namespace ZMD
{
    public class LerpScaleArrayMono : MonoBehaviour
    {
        [SerializeField] LerpScaleArray process;
        public void Input(float value) { process.Input(value); }
    }
    
    [Serializable]
    public class LerpScaleArray
    {
        public LerpScale[] scalars;
        
        public void Input(float value) 
        {
            foreach (var scalar in scalars)
                scalar.percent = value; 
        }        
    }
}
