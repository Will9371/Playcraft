using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD.Dialog
{
    public class ActorMono : MonoBehaviour
    {
        DialogController dialog => narrativeHub.dialog.process;
        NarrativeHub narrativeHub => NarrativeHub.instance;

        public ActorInfo self;
        public List<Relationship> relationships;
        public EventOfInterest[] eventsOfInterest;
        
        public void Start()
        {
            dialog.onTriggerEvent += RespondToEvent;
            narrativeHub.systemRefresh += RefreshRelationships;
            
            allNarrativeProgress = new List<NarrativeProgress>();
            foreach (var narrative in self.narratives)
                allNarrativeProgress.Add(new NarrativeProgress(narrative));
        }
        
        public void OnDestroy()
        {
            if (NarrativeHub.exists) 
            {
                dialog.onTriggerEvent -= RespondToEvent;
                narrativeHub.systemRefresh -= RefreshRelationships;
            }
        }
        
        void RespondToEvent(SO eventId)
        {
            foreach (var occasion in eventsOfInterest)
                occasion.RequestActivate(eventId);
        }
        
        public void OnClick() => dialog.Begin(GetStartingDialog(GetRelationshipToPlayer()));

        Relationship activeRelationship;
        
        DialogNode GetStartingDialog(Relationship relationship)
        {
            activeRelationship = relationship;
            var nextNarrative = GetNextNarrative(relationship);
            if (nextNarrative.value.onceOnly) nextNarrative.locked = true;
            return nextNarrative.value.startingNode;
        }
        
        [SerializeField]
        List<NarrativeProgress> allNarrativeProgress;
        NarrativeProgress GetNextNarrative(Relationship relationship)
        {
            foreach (var narrative in allNarrativeProgress)
                if (narrative.IsAvailable(relationship) && !narrative.locked)
                    return narrative;
                        
            Debug.LogError("No available narrative");
            return null;
        }
        
        Relationship GetRelationshipToPlayer()
        {
            foreach (var relationship in relationships)
                if (relationship.actor == narrativeHub.player)
                    return relationship;
        
            Debug.LogError("GetRelationshipToPlayer returning null");
            return null;
        }
        
        void RefreshRelationships()
        {
            //Debug.Log($"{self.displayName} perceives that: ");
            foreach (var relationship in relationships)
                relationship.ResetReputation();
        
            foreach (var ownRelationship in relationships)
            {
                var otherRelationships = narrativeHub.GetActor(ownRelationship.actor).relationships;
                //Debug.Log($"{ownRelationship.name}, whom they like as {ownRelationship.baseValues.favor}, likes");

                foreach (var otherRelationship in otherRelationships)
                {
                    //Debug.Log($"{otherRelationship.name} as {otherRelationship.baseValues.favor}");
                    ModifyRelationship(ownRelationship, otherRelationship);
                }
            }
        }
        
        void ModifyRelationship(Relationship ownToOther, Relationship otherToOther)
        {
            //Debug.Log($"{self.name} assessing {ownToOther.name}'s relationship to {otherToOther.name}...");
            if (otherToOther.actor == self)
                ownToOther.AddReputation(otherToOther.baseValues, self.mirroring);
            
            foreach (var ownRelationship in relationships)
            {
                if (otherToOther.actor == ownRelationship.actor)
                {
                    //Debug.Log($"Updating {self.name}'s relationship to {ownRelationship.name} " +
                    //          $"based on {self.name}'s relationship to {ownToOther.name}");
                    ownRelationship.AddReputation(otherToOther.baseValues, self.empathy, ownToOther.baseValues);
                }
            }
        }

        public void GainAffection(float value) => activeRelationship.affection += value;
        public void GainFear(float value) => activeRelationship.fear += value;
        public void GainTrust(float value) => activeRelationship.trust += value;

        [Serializable]
        public class EventOfInterest
        {
            public SO eventId;
            public UnityEvent response;
            public bool onceOnly;
            public bool locked;
            
            public void RequestActivate(SO eventId)
            {
                if (eventId != this.eventId || locked) return;
                response.Invoke();
                if (onceOnly) locked = true;
            }
        }
        
        [Serializable]
        public class NarrativeProgress
        {
            public Narrative value;
            public bool locked;
            
            public NarrativeProgress(Narrative narrative)
            {
                value = narrative;
                locked = false;
            }
            
            public ActorInfo actor => value.requirement.actor;
            public bool IsAvailable(Relationship relationship) => value.IsAvailable(relationship);
        }
    }
}