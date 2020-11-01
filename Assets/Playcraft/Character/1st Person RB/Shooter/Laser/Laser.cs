using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.FPS
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] LaserData data;
        [SerializeField] UnityEvent OnActivate;
        
        Vector3 startPosition;
        
        void OnEnable()
        {
            startPosition = transform.position;
        }
        
        public void SetData(LaserData value) { data = value; }
            
        void Update()
        {
            transform.Translate(Vector3.forward * data.speed * Time.deltaTime);
            var travelDistance = Vector3.Distance(startPosition, transform.position);
            if (travelDistance > data.range) Activate();
        }
        
        public void Activate()
        {
            OnActivate.Invoke();
            gameObject.SetActive(false);        
        }
    }
}
