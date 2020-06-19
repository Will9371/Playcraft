using UnityEngine;

namespace Playcraft
{
    public class MoveTowards : MonoBehaviour
    {
        public void Input(Vector3 self, Vector3 target, float speed)
        {
            transform.position = Vector3.MoveTowards(self, target, speed * Time.deltaTime);
        }
    }
}
