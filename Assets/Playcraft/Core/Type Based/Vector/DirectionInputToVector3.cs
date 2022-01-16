using UnityEngine;

namespace Playcraft
{
    public class DirectionInputToVector3 : MonoBehaviour
    {
        [SerializeField] Vector3Event Output;
        
        Vector3 direction = Vector3.zero;

        void Update()
        {
            Output.Invoke(direction);
            direction = Vector3.zero;
        }
        
        public void Forward() { direction += Vector3.forward; }
        public void Back() { direction += Vector3.back; }
        public void Left() { direction += Vector3.left; }
        public void Right() { direction += Vector3.right; }
        public void Up() { direction += Vector3.up; }
        public void Down() { direction += Vector3.down; }
    }
}
