﻿using UnityEngine;

// DEPRECATE: trivial functionality
namespace ZMD
{
    public class MoveTransform : MonoBehaviour
    {
        public void MoveStep(Vector3 value) { transform.Translate(value * Time.deltaTime); }
        public void MoveStep(Vector3SO value) { MoveStep(value.value); }
    }
}
