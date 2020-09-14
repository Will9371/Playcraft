using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    [CreateAssetMenu(menuName = "Playcraft/VR/Swing State")]
    public class SwingState : ScriptableObject
    {
        public Vector3SO direction;
        public NextState[] nextStates;
        [SerializeField] bool autoNormalize = true;
        
        NormalizedPercent distribution = new NormalizedPercent();
        float[] chances;
        
        public SwingState GetRandom()
        {
            return nextStates[distribution.GetRandomIndex()].state;
        }
            
        void OnValidate()
        {
            if (nextStates.Length == 0 || !autoNormalize) return;
            NormalizeProbabilities();
        }
        
        void NormalizeProbabilities()
        {
            // Verify initialization
            if (chances == null || chances.Length != nextStates.Length)
                chances = new float[nextStates.Length];
            
            // Extract values for normalization from local data
            for (int i = 0; i < nextStates.Length; i++)
                chances[i] = nextStates[i].chance;
            
            // Calculate normalized values
            distribution.Normalize(chances);
            
            // Apply normalization to local data
            for (int i = 0; i < nextStates.Length; i++)
                nextStates[i].chance = distribution.values[i];
        }
        
        [Serializable] public class NextState
        {
            public SwingState state;
            [Range(0f, 1f)] public float chance;
        }
    }
}
