using UnityEngine;

namespace Playcraft
{
    public class StretchedWire : MonoBehaviour
    {
        public Transform start, end;
        
        void Update() { Stretch(); }
        void OnValidate() { Stretch(); }
        
        Vector3 priorStartPosition;
        Vector3 priorEndPosition;
        bool startHasMoved => start.position != priorStartPosition;
        bool endHasMoved => end.position != priorEndPosition;

        public void Stretch() 
        {
            if (!start || !end) return;
            if (!startHasMoved && !endHasMoved) return;
            
            Stretch(start, end);
            priorStartPosition = start.position;
            priorEndPosition = end.position;           
        } 
        
        void Stretch(Transform t1, Transform t2)
        {
            //Debug.Log("StretchWire method reached " + t1.position + ", " + t2.position);
            transform.eulerAngles = TransformLine.Span2Nodes(transform, t1, t2);
            transform.Rotate(90f, 0f, -90f);

            float newYscale = Vector3.Distance(t1.position, t2.position) / 2f;
            transform.localScale = new Vector3(transform.localScale.x, newYscale, transform.localScale.z);
            transform.position = TransformLine.Midpoint(t1.position, t2.position);
        }
    }
}
