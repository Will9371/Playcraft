using System;
using UnityEngine;

namespace Playcraft
{
    namespace Pooling
    {
        [Serializable]
        public class ObjectPoolData 
        {
            public GameObject prefab;
            public int startSize;
        }
    }
}
