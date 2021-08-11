using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class SwingDataTracker
    {
        [SerializeField] AccumulateVelocity velocity;
        [SerializeField] AccumulateVelocity edge;
        
        public SwingData GetSwingData() 
        { 
            return new SwingData(velocity.averageMagnitude, edge.averageDirection, edge.edgeAlignment); 
        }
    }
}
