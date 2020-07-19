using UnityEngine;

public class SetFramerate : MonoBehaviour
{
    [SerializeField] int targetFrameRate = -1;
    
    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
