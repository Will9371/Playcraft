using System;

// Tracks whether a constant set of effects are active
namespace Playcraft
{
    [Serializable] public class PotentialBoolEffects
    {
        public Effect[] stack;

        public bool anyActive
        {
            get 
            {
                foreach (var item in stack) 
                    if (item.active)
                        return true;
                    
                return false;
            }
        }
        
        public bool allActive
        {
            get
            {
                foreach (var item in stack)
                    if (!item.active)
                        return false;
                        
                return true;
            }
        }
        
        // * Needs Verification
        public void SetEffectActiveForTime(SO id, float duration)
        {
            SetEffectActive(id, true);
            MonoBehaviourExtensions.ExecuteAfterTime(DeactivateCachedEffect, duration);
        }
        
        SO cachedEffectId;
        void DeactivateCachedEffect() { SetEffectActive(cachedEffectId, false); }
            
        public void SetEffectActive(SO id, bool value)
        {
            var item = GetEffectItem(id);
            if (item != null) item.active = value;             
        }
            
        Effect GetEffectItem(SO id)
        {
            foreach (var item in stack)
                if (item.id == id)
                    return item;
                        
            return null;
        }

        [Serializable] public class Effect
        {
            public SO id;
            public bool active;
        }
    }
}
