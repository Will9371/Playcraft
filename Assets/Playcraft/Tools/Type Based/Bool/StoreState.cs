using UnityEngine;

namespace Playcraft
{
    public class StoreState : MonoBehaviour
    {
        bool state;
        public void SetState(bool value) { state = value; }
        public void CheckState() { Output.Invoke(state); }
        public BoolEvent Output;
    }
}
