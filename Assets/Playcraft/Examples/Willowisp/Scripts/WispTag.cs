using System;
using UnityEngine;

namespace Playcraft
{
    namespace Examples
    {
        namespace Willowisp
        {
            public class WispTag : MonoBehaviour
            {
                public WispType value;
            }

            public enum WispType
            {
                Blue, Red, Green, Yellow
            }
            
            [Serializable] public struct BlockerData
            {
                public Material material;
                public WispType type;
            }  
        }
    }
}
