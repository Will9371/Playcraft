using UnityEngine;

public class LerpLocalPosition : MonoBehaviour
{
    [SerializeField] int defaultIndex;
    [SerializeField] bool useLocal;
    [SerializeField] Vector3[] positions;

    public int index;    
    public Vector3 start;
    public Vector3 end;    
        
    void Start()
    {
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
        if (useLocal) transform.localPosition = position;
        else transform.position = position;
    }
}
