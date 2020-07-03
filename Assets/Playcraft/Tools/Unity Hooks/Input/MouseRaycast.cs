using UnityEngine;

public class MouseRaycast : MonoBehaviour
{
    [SerializeField] RaycastHitEvent Output;
    [SerializeField] LayerMask layerMask;
    [SerializeField] QueryTriggerInteraction triggerInteraction;

    public void GetHit()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, triggerInteraction))
            Output.Invoke(hit);
    }
}
