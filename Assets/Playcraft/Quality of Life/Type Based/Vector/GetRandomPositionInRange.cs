using System;
using UnityEngine;

// ADDED
public class GetRandomPositionInRange : MonoBehaviour
{
    [SerializeField] MinMaxVector3 range;
    [SerializeField] Vector3Event Output;
    
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