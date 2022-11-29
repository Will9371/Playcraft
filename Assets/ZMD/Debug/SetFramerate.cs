using UnityEngine;

public class SetFramerate : MonoBehaviour
{
    [SerializeField] int targetFrameRate = -1;
    void Start() => Application.targetFrameRate = targetFrameRate;
}
