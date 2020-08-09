using UnityEngine;

namespace Playcraft
{
    public class GetTargetVector : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] bool otherIsTarget;
        [SerializeField] Vector3Event Output;
        #pragma warning restore 0649
        
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
