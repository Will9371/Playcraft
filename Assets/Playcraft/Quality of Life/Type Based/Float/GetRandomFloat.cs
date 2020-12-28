using UnityEngine;

public class GetRandomFloat : MonoBehaviour
{
    [SerializeField] Vector2 range;
    [SerializeField] FloatEvent Output;
    public void Input() { Output.Invoke(Random.Range(range.x, range.y)); } 
}
