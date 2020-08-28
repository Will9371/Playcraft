using System;
using UnityEngine;

namespace Playcraft.Pooling
{
    [Serializable] public class ObjectPoolData 
    {
        public GameObject prefab;
        public int startSize;
        public bool isOnCanvas;
    }
}