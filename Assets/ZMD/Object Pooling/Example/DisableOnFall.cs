using UnityEngine;

namespace ZMD
{
    public class DisableOnFall : MonoBehaviour
    {
        [SerializeField] float disableHeight = -10f;
    
        void Update()
        {
            if (transform.position.y < disableHeight)
                gameObject.SetActive(false);
        }
    }
}