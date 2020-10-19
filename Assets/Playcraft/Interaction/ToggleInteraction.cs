using UnityEngine;

public class ToggleInteraction : MonoBehaviour
{
    [SerializeField] TagSO activate;
    [SerializeField] TagSO deactivate;

    TagSO message => isActive ? deactivate : activate;
    GameObject source => isActive ? null : gameObject;

    ISetObject objectRelay;
    IMessage interactable;
    bool isActive;

    public void SetInteractable(Collider value) 
    {
        var _interactable = value.GetComponent<IMessage>();
        if (_interactable == null) return;
        interactable = _interactable;
        objectRelay = value.GetComponent<ISetObject>();
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
        if (interactable == null) return;
        if (objectRelay != null) objectRelay.SetObject(source); 
        interactable.Message(message);
        isActive = !isActive;
    }
}
