using Playcraft;
using UnityEngine;

public class SetRotation : MonoBehaviour
{
    [SerializeField] float[] rotations;
    [SerializeField] Axis axis;
    
    public void Input(int index)
    {
        var x = transform.rotation.eulerAngles.x;
        var y = transform.rotation.eulerAngles.y;
        var z = transform.rotation.eulerAngles.z;
        
        switch (axis)
        {
            case Axis.X: x = rotations[index]; break;
            case Axis.Y: y = rotations[index]; break;
            case Axis.Z: z = rotations[index]; break;
        }
        
        transform.eulerAngles = new Vector3(x, y, z);
    }
}
