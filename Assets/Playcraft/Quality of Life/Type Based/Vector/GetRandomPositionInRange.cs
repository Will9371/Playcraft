using System;
using UnityEngine;

namespace Playcraft
{
    public class GetRandomPositionInRange : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] MinMaxVector3 range;
        [SerializeField] Vector3Event Output;
        #pragma warning restore 0649
        
        public void Input()
        {
            Output.Invoke(RandomStatics.RandomInRange(range));
        }
    }

    [Serializable] public struct MinMaxVector3
    {
        public Vector3 minimum;
        public Vector3 maximum;
    }
}