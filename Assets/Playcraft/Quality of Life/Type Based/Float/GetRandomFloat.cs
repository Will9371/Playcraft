using UnityEngine;

public class GetRandomFloat : MonoBehaviour
{
    [SerializeField] bool triggerOnStart;
    [SerializeField] Vector2 range;
    [SerializeField] FloatEvent Output;
    
    public void Start() { if (triggerOnStart) Input(); }
    public void Input() { Output.Invoke(Random.Range(range.x, range.y)); } 
}
