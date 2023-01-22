using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZMD.Dialog
{
    [CreateAssetMenu(menuName = "ZMD/Dialog/Actor")]
    public class ActorInfo : ScriptableObject
    {
        public string displayName;
        
        [Tooltip("Dialog trees, unlockable based on relationship to player")]
        public List<Narrative> narratives;
        
        [Tooltip("Extent to which others' opinions of self affect own opinions of others")]
        public RelationshipParameters mirroring;
        
        [Tooltip("Extent to which others' opinions of others affect own opinions of others")]
        public RelationshipParameters empathy;
        
        public Concern[] concerns;
        
        public void RegisterConcerns()
        {
            foreach (var concern in concerns)
            {
                concern.onTrigger += TriggerOccasion;
                concern.RegisterOccasions();
            }
        }
        
        public void UnregisterConcerns()
        {
            foreach (var concern in concerns)
            {
                concern.onTrigger -= TriggerOccasion;
                concern.UnregisterOccasions();
            }
        }
        
        void TriggerOccasion(OccasionInfo occasion, RelationshipParameters impact) => 
            onTriggerOccasion?.Invoke(occasion, impact); 
        public Action<OccasionInfo, RelationshipParameters> onTriggerOccasion;
    }

    [Serializable]
    public class Narrative
    {
        public Relationship requirement;
        public DialogNode startingNode;
        public bool IsAvailable(Relationship relationship) => requirement.MeetsThresholds(relationship);
        public bool onceOnly;
    }
    
    [Serializable]
    public class Concern
    {
        public Binding[] bindings;
    
        [Serializable]
        public class Binding
        {
            public OccasionInfo occasion;
            public RelationshipParameters impact;
            
            public void RegisterOccasion() => occasion.onTrigger += Trigger;
            public void UnregisterOccasion() => occasion.onTrigger -= Trigger;
            void Trigger() => onTrigger?.Invoke(occasion, impact); 
            public Action<OccasionInfo, RelationshipParameters> onTrigger;
        }
        
        public void RegisterOccasions()
        {
            foreach (var binding in bindings)
            {
                binding.RegisterOccasion();
                binding.onTrigger += Trigger;
            }
        }
        
        public void UnregisterOccasions()
        {
            foreach (var binding in bindings)
            {
                binding.UnregisterOccasion();
                binding.onTrigger -= Trigger;
            }
        }
        
        void Trigger(OccasionInfo occasion, RelationshipParameters impact) => onTrigger?.Invoke(occasion, impact); 
        public Action<OccasionInfo, RelationshipParameters> onTrigger;
    }
}