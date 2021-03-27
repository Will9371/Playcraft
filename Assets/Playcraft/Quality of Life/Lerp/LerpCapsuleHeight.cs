using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpCapsuleHeight : MonoBehaviour
{
    [SerializeField] CapsuleCollider capsule;
    [SerializeField] int defaultIndex;
    [SerializeField] float[] heights;
    [SerializeField] [Range(0, 1)] float yAnchor = 0.5f;

    int index;    
    float start;
    float end;    
        
    void Start()
    {
        index = defaultIndex;
        start = heights[defaultIndex];
    }
    
    public void SetScaleIndex(int newIndex)
    {
        //if (index == newIndex) return;
        start = heights[index];
        end = heights[newIndex];
        index = newIndex;
    }
    
    float priorHeight;
    float heightStep => capsule.height - priorHeight;

    // Call continuously to move over time
    public void Input(float percent)
    {
        priorHeight = capsule.height;
        capsule.height = Mathf.Lerp(start, end, percent);
        
        transform.position += (.5f - yAnchor) * heightStep * Vector3.up;
    }
}
