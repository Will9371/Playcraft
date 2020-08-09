using UnityEngine;

namespace Playcraft
{
    public class JumpToPositionAccess : MonoBehaviour
    {
        [SerializeField] Transform destination;

        public void SetPosition(Transform other) { SetPosition(other.gameObject); }
        public void SetPosition(GameObject other)
        {
            var jumper = other.GetComponent<JumpToPosition>();
            if (jumper) jumper.SetPosition(destination);        
        }
        
        public void SetDestination(Transform value) { destination = value; }
        public void SetDestination(GameObject value) { destination = value.transform; }
    }
}
