﻿using System;
using UnityEngine;

namespace ZMD
{
    /// Get the X, Y, or Z component of a Vector
    public class GetFloatFromVector : MonoBehaviour
    {
        [SerializeField] IOData[] bindings;

        public void Input(Vector3 input)
        {
            foreach (var binding in bindings)
                binding.Input(input);
        }
        
        public void Input(Vector2 input)
        {
            foreach (var binding in bindings)
                binding.Input(input);
        }
        
        [Serializable] class IOData
        {
            public Axis inputAxis;
            public FloatEvent Output;
            
            public void Input(Vector3 input)
            {
                switch (inputAxis)
                {
                    case Axis.X: Output.Invoke(input.x); break;
                    case Axis.Y: Output.Invoke(input.y); break;
                    case Axis.Z: Output.Invoke(input.z); break;
                    default: Output.Invoke(0); break;
                }
            }
        
            public void Input(Vector2 input)
            {
                switch (inputAxis)
                {
                    case Axis.X: Output.Invoke(input.x); break;
                    case Axis.Y: Output.Invoke(input.y); break;
                    default: Output.Invoke(0); break;
                }
            }
        } 
    }
}

