using UnityEngine;

namespace Playcraft
{
    public class GetTargetVector : MonoBehaviour
    {
        [SerializeField] bool otherIsTarget;
        [SerializeField] Vector3Event Output;
        
        Vector3 ownPosition => transform.position;
        
        public void Input(GameObject other) { Input(other.transform.position); } 
        public void Input(Transform other) { Input(other.position); }
        public void Input(Vector3 otherPosition)
        {
            var vector = otherIsTarget ? otherPosition - ownPosition : ownPosition - otherPosition;
            Output.Invoke(vector);
        }
    }
}
