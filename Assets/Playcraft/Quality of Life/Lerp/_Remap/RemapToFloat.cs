using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class RemapToFloat
    {
        [SerializeField] Vector2 input;
        [SerializeField] Vector2 output;

        public float Remap(float value)
        {
            var percent = Mathf.InverseLerp(input.x, input.y, value);
            return Mathf.Lerp(output.x, output.y, percent);
        }
    }
}
