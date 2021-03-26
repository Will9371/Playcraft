using UnityEngine;

namespace Playcraft
{
    public class SetObjectsActive : MonoBehaviour
    {
        [Tooltip("Activates on input true, deactivates on input false")]
        [SerializeField] GameObject[] activateWithValue;
        [Tooltip("Activates on input false, deactivates on input true")]
        [SerializeField] GameObject[] activateAgainstValue;
        
        public void Input(bool value)
        {
            foreach (var obj in activateWithValue)
                obj.SetActive(value);
            foreach (var obj in activateAgainstValue)
                obj.SetActive(!value);
        }
    }
}
