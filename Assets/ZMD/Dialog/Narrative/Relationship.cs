using System;
using UnityEngine;

namespace ZMD.Dialog
{
    [Serializable]
    public class Relationship
    {
        public ActorInfo actor;
        public string name => actor.displayName;
        
        public RelationshipParameters baseValues;
        
        [HideInInspector]
        public RelationshipParameters reputationBonus;
        
        public float affection 
        {
            get => baseValues.affection.value + reputationBonus.affection.value;
            set => baseValues.affection.value = value;
        }
        public float fear
        {
            get => baseValues.fear.value + reputationBonus.fear.value;
            set => baseValues.fear.value = value;
        }
        public float trust
        {
            get => baseValues.trust.value + reputationBonus.trust.value;
            set => baseValues.trust.value = value;
        }
        
        public bool MeetsThresholds(Relationship relationship) =>
            relationship.affection >= affection &&
            relationship.fear >= fear &&
            relationship.trust >= trust;

        public void ResetReputation() => reputationBonus.Reset();
        
        public void AddReputation(RelationshipParameters other, RelationshipParameters empathy, RelationshipParameters source = null) => 
            reputationBonus.Add(other, empathy, source);
    }

    [Serializable]
    public class RelationshipParameters
    {
        [Tooltip("Positive = love, negative = hate, zero = neutral")]
        public RelationshipParameter affection;
        
        [Tooltip("Positive = self commands other, negative = self is subordinate to other, zero = equal status")]
        public RelationshipParameter fear;
        
        [Tooltip("Positive = trusting, negative = distrustful, zero = unfamiliar")]
        public RelationshipParameter trust;
        
        public void Reset()
        {
            affection.Reset();
            fear.Reset();
            trust.Reset();
        }
        
        public void Add(RelationshipParameters other, RelationshipParameters empathy, RelationshipParameters source = null)
        {
            affection.Add(other.affection, empathy.affection, source?.affection);
            fear.Add(other.fear, empathy.fear, source?.fear);
            trust.Add(other.trust, empathy.trust, source?.trust);
        }
    }

    [Serializable]
    public class RelationshipParameter
    {
        [Range(-1, 1)] public float value;
        
        public void Reset() => value = 0;
        
        public void Add(RelationshipParameter other, RelationshipParameter empathy, RelationshipParameter source = null)
        {
            var change = other.value * empathy.value;
            if (source != null) change *= source.value;
            value += change;
        }
    }
}
