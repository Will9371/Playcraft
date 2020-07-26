using UnityEngine;

namespace Playcraft
{
    public class CycleColor : MonoBehaviour
    {
        [SerializeField] new Renderer renderer = default;
        [SerializeField] Color[] colors = default;
        
        int index;
        
        void Start()
        {
            if (renderer == null) renderer = GetComponent<Renderer>();
        }
        
        public void Cycle()
        {
            index++;
            if (index >= colors.Length) index = 0;
            renderer.material.color = colors[index];
        }
        
        public void Set(int index)
        {
            if (index >= colors.Length) return;
            this.index = index;
            renderer.material.color = colors[index];
        }
    }
}