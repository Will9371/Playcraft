using UnityEngine;

public class GetOtherPosition : MonoBehaviour, IPosition
{
    [SerializeField] Transform other = default;
    public Vector3 position 
    {
        get => other.position;
        set { }
    }
}
