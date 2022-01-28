using System;
using UnityEngine;

namespace ZMD.Pooling
{
    [Serializable] public class ObjectPoolData 
    {
        public GameObject prefab;
        public int startSize;
        public bool isOnOverlayCanvas;
    }
}