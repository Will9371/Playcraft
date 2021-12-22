using System;
using UnityEngine;

public class ValidateCurveTest : MonoBehaviour
{
    public float number;          // Value propagates to other locations as expected.
    public AnimationCurve curve;  // Cannot be changed in editor - error?
    
    public TestCurveStruct testStruct;
    public TestCurveClass testClass;
    public TestCurveClassMethod testClass1;

    void OnValidate()
    {
        // Works as expected
        testStruct.number = number;
        testClass.number = number;
    
        // Commenting out both of these lines allows curve variable in this class to be changed in editor
        //testStruct.curve = curve;
        //testClass.curve = curve;
        
        testClass1.SetValues(curve, number);
    }
}

[Serializable] public struct TestCurveStruct 
{ 
    public AnimationCurve curve; 
    public float number; 
}

[Serializable] public class TestCurveClass 
{ 
    public AnimationCurve curve; 
    public float number; 
}

[Serializable] public class TestCurveClassMethod
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float number;
    
    public void SetValues(AnimationCurve curve, float number)
    {
        this.curve = curve;
        this.number = number;
    }
}
