using UnityEngine;

namespace Playcraft
{
    public class StretchedWire : MonoBehaviour
    {
        public Transform start, end;
        
        private void OnValidate()
        {
            if (!start || !end) return;
            Stretch(start, end);
        }
        
        private void Update()
        {
            if (!start || !end) return;
            Stretch(start, end);
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
