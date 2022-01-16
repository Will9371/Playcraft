using UnityEngine;

namespace Playcraft
{
    public class DirectionInputToVector2 : MonoBehaviour
    {
        [SerializeField] Vector2Event Output;
        
        Vector2 direction = Vector2.zero;

        void Update()
        {
            Output.Invoke(direction);
            direction = Vector2.zero;
        }
        
        public void Left() { direction += Vector2.left; }
        public void Right() { direction += Vector2.right; }
        public void Up() { direction += Vector2.up; }
        public void Down() { direction += Vector2.down; }
    }
}
