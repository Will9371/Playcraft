using UnityEngine;

public class DetectObject : MonoBehaviour
{    
    public void OnEnter(Collider other) { RequestSetComponent(other.gameObject); }
    public void OnExit(Collider other) { RequestUnsetComponent(other.gameObject); }
    
    protected virtual void RequestSetComponent(GameObject other) { }
    protected virtual void RequestUnsetComponent(GameObject other) { }
}
