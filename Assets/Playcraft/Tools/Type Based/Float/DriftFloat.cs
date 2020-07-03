using Playcraft;
using UnityEngine;

public class DriftFloat : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] FloatEvent Output;

    public float value;
    public float desiredValue;
    public void SetDesiredValue(float value) { desiredValue = value; }

    private void Update()
    {
        value = VectorMath.MoveTowards(value, desiredValue, speed);
        Output.Invoke(value);
    }
}
