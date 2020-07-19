using UnityEngine;

public class RelayAccess : MonoBehaviour
{
    public void Input(RaycastHit hit)
    {
        var relay = hit.collider.GetComponent<Relay>();
        if (relay != null) relay.Input();
    }
}
