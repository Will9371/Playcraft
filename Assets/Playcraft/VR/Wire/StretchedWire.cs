using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class StretchedWire
    {
        public Transform self;
        [Tooltip("If set, will override start position")]
        public Transform start;
        [Tooltip("If set, will override end position")]
        public Transform end;
        public Vector3 startPosition, endPosition;

        Vector3 priorStartPosition;
        Vector3 priorEndPosition;
        bool startHasMoved => startPosition != priorStartPosition;
        bool endHasMoved => endPosition != priorEndPosition;
        
        public void Stretch(Vector3 startPosition, Vector3 endPosition)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            Stretch();
        }

        public void Stretch() 
        {
            if (!self) return;
            if (start) startPosition = start.position;
            if (end) endPosition = end.position;
            if (!startHasMoved && !endHasMoved) return;
            
            self.eulerAngles = TransformLine.Span2Nodes(self, startPosition, endPosition);
            self.Rotate(90f, 0f, -90f);

            float newYscale = Vector3.Distance(startPosition, endPosition) / 2f;
            self.localScale = new Vector3(self.localScale.x, newYscale, self.localScale.z);
            self.position = TransformLine.Midpoint(startPosition, endPosition);   

            priorStartPosition = startPosition;
            priorEndPosition = endPosition;
        } 
        
        public void SetActive(bool value) { self.gameObject.SetActive(value); } 
    }
}
