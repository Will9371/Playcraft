using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintToConsole : MonoBehaviour
{
    public void Print(Vector2 value)
    {
        Print(value.ToString("F4"));
    }

    public void Print(string message)
    {
        Debug.Log(message);
    }
}
