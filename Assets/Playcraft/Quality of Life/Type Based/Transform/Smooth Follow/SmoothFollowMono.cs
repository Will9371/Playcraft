using UnityEngine;

public class SmoothFollowMono : MonoBehaviour
{
    [SerializeField] SmoothFollow process;
    void OnValidate() { process.OnValidate(); }
    void Update() { process.Update(); }
    public void SetTarget(Transform value) { process.SetTarget(value); }
}