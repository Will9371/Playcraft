using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Playcraft
{
    /// Allows multiple sources to get non-overlapping integers from a range
    [Serializable]
    public class ReserveInt
    {
        [Tooltip("Include all integers in range (inclusive, rounded)")]
        [SerializeField] Vector2 range;
        [Tooltip("Allows same source to get same value twice (or more) in a row")]
        [SerializeField] bool allowRepeat;
        
        ReservedValue[] values;
        
        bool initialized;
        
        void Initialize()
        {
            var min = Mathf.RoundToInt(range.x);
            var max = Mathf.RoundToInt(range.y);
            var length = max - min;
            values = new ReservedValue[length];            
            
            for (int i = 0; i < length; i++)
                values[i] = new ReservedValue(i + min);
        }

        public int GetAvailableValue(int sourceId)
        {
            if (!initialized) 
            {
                Initialize();
                initialized = true;
            }
        
            // Generate a list of valid values
            var validValues = new List<ReservedValue>();
            
            foreach (var value in values)
            {
                // Clear existing reservations from incoming source
                if (value.ownerId == sourceId)
                {
                    value.ClearOwner();
                    if (!allowRepeat) continue;
                }
            
                if (value.ownerId < 0)
                    validValues.Add(value);
            }
            
            // Exit early if list is empty
            if (validValues.Count <= 0) return -1;

            // Get the result
            var index = Random.Range(0, validValues.Count);
            var result = validValues[index].value;
            
            // Flag the entry as reserved
            foreach (var value in values)
                if (value.value == result)
                    value.SetOwner(sourceId);
            
            return result;
        }
        
        [Serializable]
        class ReservedValue
        {
            public int value;
            public int ownerId;
            public bool reserved => ownerId >= 0;
            
            public ReservedValue(int value)
            {
                this.value = value;
                ownerId = -1;
            }
            
            public void ClearOwner() { SetOwner(-1); }
            public void SetOwner(int ownerId) { this.ownerId = ownerId; }
        }
    }
}