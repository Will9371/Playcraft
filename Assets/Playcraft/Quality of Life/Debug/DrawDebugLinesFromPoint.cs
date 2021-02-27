using UnityEngine;

namespace Playcraft
{
    public class DrawDebugLinesFromPoint : MonoBehaviour
    {
        [SerializeField] Transform source;
        [SerializeField] [Range(0f, 360f)] float maxAngle = 45f;
        [SerializeField] Color color;
        [SerializeField] float length;
        [SerializeField] Vector3[] axes;
        [SerializeField] FloatEvent SyncMaxAngle;
        
        Draw_Debug_Lines_From_Point process = new Draw_Debug_Lines_From_Point();
        
        void OnValidate() { SyncMaxAngle.Invoke(maxAngle); }
        void OnDrawGizmos() { process.OnDrawGizmos(source, maxAngle, axes, color, length); }
    }

    public class Draw_Debug_Lines_From_Point
    {
        public void OnDrawGizmos(Transform source, float maxAngle, Vector3[] axes, 
            Color color, float length = 1f)
        {
            Gizmos.color = color;
            foreach (var axis in axes)
            {
                DrawRay(source, maxAngle/2f, axis, length); 
                DrawRay(source, -maxAngle/2f, axis, length);     
            } 
        }
            
        void DrawRay(Transform source, float angle, Vector3 axis, float length = 1f)
        {
            if (!source) return;
            var rotation = Quaternion.AngleAxis(angle, axis);
            var localDirection = rotation * source.forward;
            Gizmos.DrawRay(source.position, localDirection * length);            
        }
    }
}
