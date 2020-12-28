using UnityEngine;

public class LerpScale : MonoBehaviour
{
    [SerializeField] int defaultIndex;
    [SerializeField] Vector3[] scales;
    [SerializeField] [Range(0, 1)] float xAnchor = 0.5f;
    [SerializeField] [Range(0, 1)] float yAnchor = 0.5f;
    [SerializeField] [Range(0, 1)] float zAnchor = 0.5f;

    int index;    
    Vector3 start;
    Vector3 end;    
        
    void Start()
    {
        index = defaultIndex;
        start = scales[defaultIndex];
    }
    
    public void SetScaleIndex(int newIndex)
    {
        //if (index == newIndex) return;
        start = scales[index];
        end = scales[newIndex];
        index = newIndex;
    }
    
    Vector3 priorScale;
    Vector3 scaleStep => transform.localScale - priorScale;

    // Call continuously to move over time
    public void Input(float percent)
    {
        priorScale = transform.localScale;
        transform.localScale = Vector3.Lerp(start, end, percent);
        
        // * Not tested
        if (xAnchor != 0.5f) transform.position += (.5f - xAnchor) * scaleStep.x * Vector3.right;
        if (yAnchor != 0.5f) transform.position += (.5f - yAnchor) * scaleStep.y * Vector3.up;
        if (zAnchor != 0.5f) transform.position += (.5f - zAnchor) * scaleStep.z * Vector3.forward;
    }
}
