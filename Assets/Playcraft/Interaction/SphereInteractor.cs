using UnityEngine;

// NOT USED
public class SphereInteractor : MonoBehaviour
{
    [SerializeField] float range = .5f;
    [SerializeField] TagSO pickupMessage;
    [SerializeField] TagSO dropMessage;

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
