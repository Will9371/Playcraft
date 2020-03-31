using System;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] KeyVector3Group[] vectorKeyBindings;
    [SerializeField] KeyAxisGroup[] axisKeyBindings;
    
    void Update()
    {        
        foreach (var binding in vectorKeyBindings)
            binding.Update();
        foreach (var binding in axisKeyBindings)
            binding.Update();
    }
}

[Serializable]
public class KeyVector3Group
{
    [SerializeField] Vector3Event broadcastVector;
    [SerializeField] KeyVector3[] vectorKeys;
    
    public void Update()
    {
        foreach(var item in vectorKeys)
            foreach (var key in item.keys)
                if (Input.GetKey(key))
                    broadcastVector.Invoke(item.vector);        
    }
}

[Serializable]
public struct KeyVector3
{
    public KeyCode[] keys;
    public Vector3 vector;
}

[Serializable]
public class KeyAxisGroup
{
    [SerializeField] AxisTypeAndBoolEvent broadcastAxis;
    [SerializeField] KeyAxis[] axisKeys;
    
    public void Update()
    {
        foreach(var item in axisKeys)
            foreach (var key in item.keys)
                if (Input.GetKey(key))
                    broadcastAxis.Invoke(item.axis, item.clockwise);         
    }
}

[Serializable]
public class KeyAxis
{
    public KeyCode[] keys;
    public Axis axis;
    public bool clockwise;
}

[Serializable] public class AxisTypeAndBoolEvent : UnityEvent<Axis, bool> { }

public enum Axis { X, Y, Z }
