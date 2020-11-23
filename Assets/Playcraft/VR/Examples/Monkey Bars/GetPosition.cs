using UnityEngine;

public interface IPosition 
{ 
    Vector3 position { get; set; } 
}

public class GetPosition : MonoBehaviour, IPosition
{    
    public Vector3 position
    {
        get => transform.position;  
        set { }
    }
}
