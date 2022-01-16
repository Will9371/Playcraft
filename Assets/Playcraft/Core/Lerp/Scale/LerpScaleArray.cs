using UnityEngine;

namespace Playcraft
{
    public class LerpScaleArray : MonoBehaviour
    {
        [SerializeField] LerpScale[] scalars;
        
        public void Input(float value) 
        {
            foreach (var scalar in scalars)
                scalar.Input(value); 
        }
    }
}
