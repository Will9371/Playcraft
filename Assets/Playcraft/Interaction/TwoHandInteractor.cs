using System;
using Playcraft;
using UnityEngine;

public class TwoHandInteractor : MonoBehaviour
{
    [SerializeField] MessageLink messenger;
    [SerializeField] TwoHandInteractor otherInteractor;
    [SerializeField] TagSO enter;
    [SerializeField] TagSO exit;
    [SerializeField] Interaction[] interactions;

    
    public void Enter(Collider other)
    {
        var link = other.GetComponent<MessageLink>();
        if (link) Enter(link);
    }
    
    void Enter(MessageLink other)
    {
        foreach (var interaction in interactions)
            interaction.potentialLink = other;
        
        messenger.Output(enter, other);
    }
    
    public void Exit(Collider other)
    {
        var link = other.GetComponent<MessageLink>();
        if (link) Exit(link);        
    }
    
    void Exit(MessageLink other)
    {
        foreach (var interaction in interactions)
            interaction.potentialLink = null;
        
        messenger.Output(exit, other);
    }
    
    public void Activate(TagSO message)
    {
        var interaction = GetInteraction(message, true);
        if (interaction == null) return;
        
        var otherInteraction = otherInteractor.GetInteraction(message, true);

        if (otherInteraction != null && 
            otherInteraction.activeLink == interaction.potentialLink)
            return;
        
        interaction.activeLink = interaction.potentialLink;
        messenger.SetLink(interaction.potentialLink);
        messenger.Output(message);
    }
    
    public Interaction GetInteraction(TagSO message, bool activeSearch)
    {
        foreach (var interaction in interactions)
            if (interaction.IsMe(message, activeSearch))
                return interaction;
        
        return null;
    }
    
    public void Deactivate(TagSO message)
    {
        var interaction = GetInteraction(message, false);
        if (interaction == null || interaction.activeLink == null) return;
            
        interaction.activeLink = null;
        messenger.Output(message);
        messenger.ClearLink();
    }
    
    [Serializable] public class Interaction
    {
        public TagSO activate;
        public TagSO deactivate;
        [NonSerialized] public MessageLink potentialLink;
        [NonSerialized] public MessageLink activeLink;
        
        public bool IsMe(TagSO query, bool isActive)
        {
            var message = isActive ? activate : deactivate;
            return message == query;
        }
    }
}
