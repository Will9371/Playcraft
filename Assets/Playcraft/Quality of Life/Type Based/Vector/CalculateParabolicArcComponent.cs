using UnityEngine;

namespace Playcraft
{
    public class CalculateParabolicArcComponent : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform source;
        [SerializeField] float range = 5f;
        [SerializeField] float gravity = 9.8f;
        [SerializeField] int resolution = 40;
        [SerializeField] float maxDrop = 5f;
        [SerializeField] Vector3ArrayEvent Output;
        [SerializeField] BoolEvent OnActivate;
        #pragma warning restore 0649
        
        CalculateParabolicArc arc;
        
        void Awake()
        {
            arc = new CalculateParabolicArc(range, gravity, resolution, maxDrop);
        }
                    
        bool active;
        public void SetActive(bool value) 
        {
            active = value;
            OnActivate.Invoke(value); 
        }

        public void Calculate()
        {
            if (!active) return;            
            transform.eulerAngles = new Vector3(0f, source.eulerAngles.y, 0f); 
            Output.Invoke(arc.Calculate(-source.localEulerAngles.x));
        }
    }
}
