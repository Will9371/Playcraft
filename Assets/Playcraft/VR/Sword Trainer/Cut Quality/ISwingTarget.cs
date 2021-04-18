﻿using UnityEngine;

namespace Playcraft
{
    public interface ISwingTarget
    {
        void SendData(SwingData data);
    }
    
    public struct SwingData
    {
        public readonly float speed;
        public readonly Vector3 direction;
        public readonly float edgeAlignment;
        
        public SwingData(float speed, Vector3 direction, float edgeAlignment)
        {
            this.speed = speed;
            this.direction = direction;
            this.edgeAlignment = edgeAlignment;
        }
    }
}