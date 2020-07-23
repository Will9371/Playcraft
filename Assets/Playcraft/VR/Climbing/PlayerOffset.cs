using UnityEngine;

namespace Playcraft.VR
{
    public class PlayerOffset : MonoBehaviour
    {
        private SphereCollider playerCol;
        [SerializeField] Transform playerHeadLocation = default;
        Vector3 headOffset;

        private void Awake()
        {
            playerCol = GetComponent<SphereCollider>();
        }

        private void Update()
        {
            headOffset = transform.position - playerHeadLocation.position;
            headOffset = new Vector3(-headOffset.x, -headOffset.y, -headOffset.z);
            playerCol.center = headOffset;
        }
    }
}
