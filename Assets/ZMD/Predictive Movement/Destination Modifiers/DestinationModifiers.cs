using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Predictive Movement/Destination Modifier/Modifier Group", fileName = "Destination Modifiers")]
    public class DestinationModifiers : ScriptableObject
    {
        public DestinationModifier[] modifiers;
        
        public Vector3 Tick(Vector3 value)
        {
            foreach (var modifier in modifiers)
                value = modifier.Tick(value);
                
            return value;
        }
    }
}