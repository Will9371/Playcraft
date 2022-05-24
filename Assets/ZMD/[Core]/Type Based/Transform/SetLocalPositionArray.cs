using UnityEngine;

namespace ZMD
{
    public class SetLocalPositionArray : MonoBehaviour
    {
        [SerializeField] Vector3 localPosition;
        [SerializeField] Transform[] locations;
        
        void OnValidate()
        {
            foreach (var location in locations)
                location.localPosition = localPosition;
        }
    }
}