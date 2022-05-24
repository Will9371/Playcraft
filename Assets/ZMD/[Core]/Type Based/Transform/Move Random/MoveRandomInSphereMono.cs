using UnityEngine;

namespace ZMD
{
    public class MoveRandomInSphereMono : MonoBehaviour
    {
        public Vector3 range;
        public void JumpToRandomPoint() { transform.localPosition = RandomStatics.RandomInScaledSphere(range); }
    }
}
