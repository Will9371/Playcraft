using UnityEngine;

public class ToggleInteraction : MonoBehaviour
{
    [SerializeField] TagSO activate;
    [SerializeField] TagSO deactivate;

    TagSO message => isActive ? deactivate : activate;
    GameObject source => isActive ? null : gameObject;

    ISetObject objectRelay;
    ISetObject cachedObjectRelay;
    IMessage interactable;
    IMessage cachedInteractable;
    bool isActive;

    public void SetInteractable(Collider value) 
    {
        var _interactable = value.GetComponent<IMessage>();
        if (_interactable == null) return;
        interactable = _interactable;
        objectRelay = value.GetComponent<ISetObject>();

        cachedInteractable = interactable;
        cachedObjectRelay = objectRelay;
    }

    public void RemoveInteractable(Collider value) 
    { 
        var _interactable = value.GetComponent<IMessage>();
        if (_interactable == null) return;
        interactable = null;
        objectRelay = null; 
    }

    public void Interact()
    {
        if (objectRelay != null) 
            objectRelay.SetObject(source); 
        else if (isActive && cachedObjectRelay != null)
            cachedObjectRelay.SetObject(source);

        if (interactable != null)
            interactable.Message(message);
        else if (isActive && cachedInteractable != null)
            cachedInteractable.Message(message);
        else
            return;

        isActive = !isActive;
    }
}
