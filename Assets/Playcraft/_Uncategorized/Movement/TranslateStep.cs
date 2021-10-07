using UnityEngine;

namespace Playcraft
{
    public class TranslateStep : MonoBehaviour
    {
        [SerializeField] Transform self;
        [SerializeField] float speed;
        
        void Awake() { if (!self) self = transform; }
        
        public void Input(Vector3 direction) { self.Translate(speed * Time.deltaTime * direction); }
    }
}
