using UnityEngine;

namespace Playcraft
{
    public class PrintToConsole : MonoBehaviour
    {
        public void Print(Vector2 value) { Print(value.ToString("F4")); }
        public void Print(Transform value) { Print(value.name); }
        public void Print(int value) { Print(value.ToString()); }
        public void Print(float value) { Print(value.ToString("F4")); }
        public void Print(string message) { Debug.Log(message); }
    }
}
