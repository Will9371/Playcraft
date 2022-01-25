﻿using UnityEngine;

namespace ZMD
{
    public class GetRandomPositionInRange : MonoBehaviour
    {
        [SerializeField] MinMaxVector3 range;
        [SerializeField] Vector3Event Output;
        
        public void Input() { Output.Invoke(RandomStatics.RandomInRectangle(range)); }
    }
}