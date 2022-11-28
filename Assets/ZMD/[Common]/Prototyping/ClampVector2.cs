﻿using UnityEngine;

namespace ZMD
{
    public class ClampVector2 : MonoBehaviour
    {
        [SerializeField] Vector2Event OnOutput;

        public void ClampNegativeYtoX(Vector2 input)
        {
            if (input.y < 0)
            {
                input.x = input.x < 0 ? -1 : 1;
                input.y = 0;
            }
            
            OnOutput.Invoke(input);
        }
    }
}
