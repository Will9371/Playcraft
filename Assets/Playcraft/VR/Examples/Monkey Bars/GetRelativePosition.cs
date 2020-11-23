using UnityEngine;

// RENAME
public class GetRelativePosition : MonoBehaviour, IPosition
{
    #pragma warning disable 0649
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    #pragma warning restore 0649

    float percentFromStart;
    
    Vector3 startToEnd => end.position - start.position;

    public Vector3 position
    {
        get => start.position + startToEnd * percentFromStart;
        set => SetPercent(value);
    }
    
    void SetPercent(Vector3 value)
    {
        var startDistance = Vector3.Distance(value, start.position);
        var endDistance = Vector3.Distance(value, end.position);
        percentFromStart = startDistance / (startDistance + endDistance);
    }
}
