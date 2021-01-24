using UnityEngine;

public class LerpLocalPosition : MonoBehaviour
{
    [SerializeField] Transform self;
    [SerializeField] int defaultIndex;
    [SerializeField] bool useLocal;
    [SerializeField] Vector3[] positions;

    public int index;    
    public Vector3 start;
    public Vector3 end;    
        
    void Start()
    {
        if (!self) self = transform;
        index = defaultIndex;
        start = positions[defaultIndex];
    }
    
    public void SetDestination(int newIndex)
    {
        start = positions[index];
        end = positions[newIndex];
        index = newIndex;
    }
    
    Vector3 position;

    // Call continuously to move over time
    public void Input(float percent)
    {
        position = Vector3.Lerp(start, end, percent);
        if (useLocal) self.localPosition = position;
        else self.position = position;
    }
}
