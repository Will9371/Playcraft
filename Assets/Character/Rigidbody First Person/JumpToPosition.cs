using UnityEngine;

public class JumpToPosition : MonoBehaviour
{
    [SerializeField] Transform[] locations;
    int index;
    
    public void CycleLocation()
    {
        SetLocation(RangeMath.CycleInt(index, locations.Length));
    }
    
    public void SetLocation(int index)
    {
        this.index = index;
        SetPosition(locations[index]);
    }
    
    public void SetPosition(Transform location)
    {
        transform.position = location.position;
    }
}
