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

    [Serializable]
    public class RemapToColor
    {
        [SerializeField] Vector2 input;
        [SerializeField] ColorRange output;

        public Color Remap(float value)
        {
            var percent = Mathf.InverseLerp(input.x, input.y, value);
            return Color.Lerp(output.start, output.end, percent);
        }
        
        [Serializable]
        public struct ColorRange
        {
            public Color start;
            public Color end;
        }
    }
}
