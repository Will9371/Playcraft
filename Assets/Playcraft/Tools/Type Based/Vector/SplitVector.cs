using System;
using UnityEngine;

namespace Playcraft
{
    public class SplitVector : MonoBehaviour
    {
        [SerializeField] SplitVectorInstance[] splitVectors = default;

        public void Split(Vector2 value)
        {
            foreach (var vector in splitVectors)
                vector.Input(value);
        }
    }

    [Serializable]
    public class SplitVectorInstance
    {
        #pragma warning disable 0649
        [SerializeField] Axis inputAxis;
        [SerializeField] Axis outputAxis;
        [SerializeField] Vector3Event OnOutput;
        #pragma warning disable 0649
        
        public void Input(float value) { Input(new Vector3(value, 0f, 0f)); }
        public void Input(Vector2 value) { Input(new Vector3(value.x, value.y, 0f)); }
        
        //public Vector3 debugInput;
        public void Input(Vector3 value)
        {
            //debugInput = value;
            switch (inputAxis)
            {
                case Axis.X: Output3(value.x); break;
                case Axis.Y: Output3(value.y); break;
                case Axis.Z: Output3(value.z); break;
                default: Debug.Log("Invalid input axis " + inputAxis); break;
            }
        }
        
        //public float debugOutput;
        public void Output3(float value)
        {
            //debugOutput = value;
            switch (outputAxis)
            {
                case Axis.X: OnOutput.Invoke(new Vector3(value, 0f, 0f)); break;
                case Axis.Y: OnOutput.Invoke(new Vector3(0f, value, 0f)); break;
                case Axis.Z: OnOutput.Invoke(new Vector3(0f, 0f, value)); break;
                default: Debug.Log("Invalid output axis " + outputAxis); break;
            }
        }
    }
}