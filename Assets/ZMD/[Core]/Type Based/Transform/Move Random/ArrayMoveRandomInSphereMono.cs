using UnityEngine;

namespace ZMD
{
    public class ArrayMoveRandomInSphereMono : MonoBehaviour
    {
        public Vector3 range;
        public Transform[] points;
        
        public void MoveAll()
        {
            foreach (var point in points)
                point.localPosition =  RandomStatics.RandomInScaledSphere(range);   
        }
    }
}
