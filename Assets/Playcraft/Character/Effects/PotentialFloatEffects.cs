using System;

// Tracks whether a constant set of effects are active
// and provides access to total value of effects
namespace Playcraft
{
    [Serializable] public class PotentialFloatEffects
    {
        public Effect[] stack;

        public float product
        {
            get
            {
                var result = 1f;
                
                foreach (var item in stack)
                    if (item.active)
                        result *= item.value;
                
                return result;
            }
        }
        
        public float sum
        {
            get
            {
                var result = 0f;
                
                foreach (var item in stack)
                    if (item.active)
                        result += item.value;
                
                return result;
            }
        }
            
        public void SetEffectActive(SO cause, bool active)
        {
            var item = GetEffectItem(cause);
            if (item != null) item.active = active;          
        }
            
        Effect GetEffectItem(SO cause)
        {
            foreach (var item in stack)
                if (item.id == cause)
                    return item;
                        
            return null;
        }

        [Serializable] public class Effect
        {
            public SO id;
            public bool active;
            public float value;
        }
    }
}
