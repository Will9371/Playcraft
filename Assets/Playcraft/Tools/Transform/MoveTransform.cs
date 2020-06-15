using UnityEngine;

public class MoveTransform : MonoBehaviour
{
    public void MoveStep(Vector3 input)
    {
        transform.Translate(input * Time.deltaTime);
    }
}
