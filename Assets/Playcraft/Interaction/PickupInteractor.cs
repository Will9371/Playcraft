using Playcraft;
using UnityEngine;
using UnityEngine.Events;

public class PickupInteractor : MonoBehaviour
{
    [SerializeField] MessageLink messenger;
    [SerializeField] TagSO handFreeMessage;
    [SerializeField] TagSO handFullMessage;
    [SerializeField] UnityEvent OnDrop;
    
    public void SetHoldingAndInteract(bool value)
    {
        SetHolding(value);
        Interact();
    }

    bool isHolding;
    public void SetHolding(bool value) 
    { 
        isHolding = value; 
        messenger.SetIgnoreLink(value);
        
        if (!value) 
        {
            messenger.ClearLink();
            OnDrop.Invoke();
        }
    }
    
    public void Interact()
    {
        var message = isHolding ? handFullMessage : handFreeMessage;
        messenger.Output(message);
    }
}
