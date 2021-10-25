using UnityEngine;

namespace Playcraft
{
    public class GetTransformFromArray : MonoBehaviour
    {
        [SerializeField] Transform[] transforms;
        [SerializeField] TransformEvent Output;
        
        public void Input(int index)
        {
            if (index < 0 || index >= transforms.Length)
                return;
        
            Output.Invoke(transforms[index]);
        }
    }
}
