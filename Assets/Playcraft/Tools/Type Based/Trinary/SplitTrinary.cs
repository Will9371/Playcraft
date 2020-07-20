using UnityEngine;

namespace Playcraft
{
    public class SplitTrinary : MonoBehaviour
    {
        [SerializeField] TrinaryEventData[] ioPaths = default;    
        
        public void Input(Trinary value)
        {
            foreach (var branch in ioPaths)
                if (branch.value == value)
                    branch.OnActivate.Invoke();
        }
    }
}