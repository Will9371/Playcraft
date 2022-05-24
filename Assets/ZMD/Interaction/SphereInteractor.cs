using UnityEngine;

// FUTURE USE
namespace ZMD
{
    public class SphereInteractor : MonoBehaviour
    {
        [SerializeField] float range = .5f;
        [SerializeField] SO pickupMessage;
        [SerializeField] SO dropMessage;

        ISetSO itemInHand;

        bool isHolding => itemInHand != null;
        Vector3 center => transform.position;

        public void Interact()
        {
            if (isHolding) Drop();
            else Pickup();
        }

        public void Pickup()
        {
            var nearby = Physics.OverlapSphere(center, range);
            Debug.Log(nearby.Length);

            foreach (var item in nearby)
            {
                var interactable = item.GetComponent<ISetSO>();
                if (interactable == null) continue;
                itemInHand = interactable;
                break;
            }

            itemInHand?.Message(pickupMessage);
        }

        public void Drop()
        {
            itemInHand?.Message(dropMessage);
            itemInHand = null;
        }
    }
}
