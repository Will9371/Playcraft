using System;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class PositionHistory
    {
        public int recordCount;
        
        Vector3[] history;
        
        int index;
        Vector3 current;
        
        public void Initialize(Vector3 start)
        {
            index = 0;
            history = new Vector3[recordCount];
            
            for (int i = 0; i < recordCount; i++)
                history[i] = start;
        }

        public Vector3 Tick(Vector3 value)
        {
            if (history == null || history.Length != recordCount)
                Initialize(value);
        
            current = history[index];
            history[index] = value;
            
            index++;
            if (index >= history.Length)
                index = 0;
            
            return current;
        }
    }
}
