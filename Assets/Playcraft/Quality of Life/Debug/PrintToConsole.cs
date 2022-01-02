using UnityEngine;

namespace Playcraft
{
    public class PrintToConsole : MonoBehaviour
    {
        public void Print(string value) { Debug.Log(value); }
        public void Print(Vector3 value) { Print(value.ToString("F4")); }
        public void Print(Vector2 value) { Print(value.ToString("F4")); }
        public void Print(Transform value) { Print(value ? value.name : "null"); }
        public void Print(int value) { Print(value.ToString()); }
        public void Print(float value) { Print(value.ToString("F4")); }
        public void Print(bool value) { Print(value.ToString()); }
        public void Print(SO value) { Print(value.name); }
        public void Print(RaycastHit value) { Print(value.point); }
        public void Print(Collider value) { Print(value ? value.name : "null"); }
        public void Print(Quaternion value) { Print(value.ToString("F3")); }
        public void Print(KeyCode value) { Print(value.ToString()); }
    }
}
