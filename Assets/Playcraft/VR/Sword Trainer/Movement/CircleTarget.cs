using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class CircleTarget
    {
        public Transform self;
        public Transform target;
        public Trinary clockwise;
        
        Vector3 targetDirection => (planarTargetPosition - self.position).normalized;
        Vector3 cross => Vector3.Cross(targetDirection, Vector3.up);
        Vector3 sideDirection => circleDirection * cross;
        Vector3 planarTargetPosition => new Vector3(target.position.x, self.position.y, target.position.z);
        float circleDirection => StaticAxis.RotationDirection(clockwise);

        public Vector3 Update() { return target ? sideDirection : Vector3.zero; }
        
        public void SetRandomDirection() { clockwise = RandomStatics.CoinToss() ? Trinary.True : Trinary.False; }
    }
}
