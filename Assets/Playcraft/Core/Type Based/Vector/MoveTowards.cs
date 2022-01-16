using UnityEngine;

namespace Playcraft
{
    public class MoveTowards : MonoBehaviour
    {
        [SerializeField] Transform self;
        [SerializeField] float speed;
        [SerializeField] bool useLocal;
        [SerializeField] bool updateInternal;
        
        Vector3 destination;
        
        float step => speed * Time.deltaTime;
        
        void Start() { if (!self) self = transform; }
        
        public void SetDestination(Vector3 value) { destination = value; }
    
        void Update()
        {
            if (!updateInternal) return;
            if (useLocal) self.localPosition = Vector3.MoveTowards(self.localPosition, destination, step);
            else self.position = Vector3.MoveTowards(self.position, destination, step);
        }
    }
}
