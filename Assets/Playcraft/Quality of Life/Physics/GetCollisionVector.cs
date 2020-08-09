using UnityEngine;

public class GetCollisionVector : MonoBehaviour
{
    [SerializeField] Vector3Event Output = default;
    
    public void Input(Collision other)
    {
        Output.Invoke(other.contacts[0].normal);
    }
}
