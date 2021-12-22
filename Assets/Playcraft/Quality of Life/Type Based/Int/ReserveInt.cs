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
        public Vector2 range;
        [Tooltip("Allows same source to get same value twice (or more) in a row")]
        public bool allowRepeat;
        
        ReservedValue[] values;
        
        bool initialized;
        
        void Initialize()
        {
            if (initialized) return;
            initialized = true;
        
            var min = Mathf.RoundToInt(range.x);
            var max = Mathf.RoundToInt(range.y);
            var length = max - min;
            values = new ReservedValue[length];            
            
            for (int i = 0; i < length; i++)
                values[i] = new ReservedValue(i + min);
        }
        
        /// Set a value if it is available, otherwise get a random available value
        public int SetIfAvailableOrGetRandom(int value, int sourceId)
        {
            if (SetIfAvailable(value, sourceId))
                return value;
            
            return GetRandomAvailable(sourceId);
        }
        
        public bool SetIfAvailable(int value, int sourceId)
        {
            var isAvailable = IsAvailable(value, sourceId);
            if (isAvailable) SetAvailable(value, sourceId);
            return isAvailable;
        }

        public bool IsAvailable(int value, int sourceId)
        {
            var reservation = GetReservedValue(value);
            return !(reservation == null || (reservation.reserved && reservation.ownerId != sourceId));
        }
        
        public void SetAvailable(int value, int sourceId)
        {
            var reservation = GetReservedValue(value);
            if (reservation == null) return;

            // Each source can only reserve one value
            foreach (var element in values)
                if (element.ownerId == sourceId)
                    element.ClearOwner();
            
            reservation.SetOwner(sourceId);
        }
        
        ReservedValue GetReservedValue(int value)
        {
            Initialize();
        
            foreach (var element in values)
                if (element.value == value)
                    return element;
                    
            return null;
        }

        public int GetRandomAvailable(int sourceId)
        {
            Initialize();
            
            // Generate a list of valid values
            var validValues = new List<ReservedValue>();
            
            foreach (var value in values)
            {
                // Clear existing reservations from incoming source
                // (assumes each source can only reserve one value)
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