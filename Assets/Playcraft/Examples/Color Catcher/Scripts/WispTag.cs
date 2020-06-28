using System;
using UnityEngine;

namespace Playcraft.Examples.ColorCatcher
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
        public int index;
        public Vector3 restPosition;
    }  
}
