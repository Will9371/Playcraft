using UnityEngine;

// FUTURE USE
namespace Playcraft
{
    public class SphereInteractor : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float range = .5f;
        [SerializeField] SO pickupMessage;
        [SerializeField] SO dropMessage;
        #pragma warning restore 0649

        IMessage itemInHand;

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
                var interactable = item.GetComponent<IMessage>();
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
