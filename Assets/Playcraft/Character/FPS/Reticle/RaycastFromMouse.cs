using UnityEngine;

namespace Playcraft
{
    public class RaycastFromMouse : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] RaycastHitEvent output;

        public void Trigger()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
                output.Invoke(hit);
        }    
    }
}
