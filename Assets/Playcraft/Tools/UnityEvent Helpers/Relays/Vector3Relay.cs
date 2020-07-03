using Playcraft;
using UnityEngine;

public class Vector3Relay : MonoBehaviour
{
    [SerializeField] Vector3Event Output = default;
    public void Input(Vector3 value) { Output.Invoke(value); }
    public void Input(Vector3SO data) { Input(data.value); }
}
