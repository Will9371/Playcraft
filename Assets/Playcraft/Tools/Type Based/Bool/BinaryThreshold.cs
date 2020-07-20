﻿using UnityEngine;
using UnityEngine.Events;

public class BinaryThreshold : MonoBehaviour
{
    [SerializeField] Vector2 thresholds;
    
    [SerializeField] UnityEvent OutputHigh;
    [SerializeField] UnityEvent OutputLow;
    
    public bool isHigh;
    //public float debugValue;

    public void Input(Vector2 value) { Input(value.magnitude); }
    public void Input(float value)
    {
        //debugValue = value;
        if (!isHigh && value > thresholds.y)
        {
            OutputHigh.Invoke();
            isHigh = true;
        }
        else if (isHigh && value < thresholds.x)
        {
            OutputLow.Invoke(); 
            isHigh = false;
        }
    }
}
