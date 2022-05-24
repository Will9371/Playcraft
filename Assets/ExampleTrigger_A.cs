using System;
using UnityEngine;

public class ExampleTrigger_A : MonoBehaviour
{
    //[SerializeField] ExampleResponse_B scriptB;
    
    public Action onStart;

    void Start()
    {
        //scriptB.DirectlyReferenced();
        onStart?.Invoke();
    }
}
