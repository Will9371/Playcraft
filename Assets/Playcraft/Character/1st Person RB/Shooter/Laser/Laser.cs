using Playcraft.Pooling;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.FPS
{
    public class Laser : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] CreateExplosion explosive;
        [SerializeField] UnityEvent OnActivate;
        #pragma warning restore 0649
        
        LaserData data;
        Vector3 startPosition;
        
        void OnEnable()
        {
            startPosition = transform.position;
        }
        
        public void SetData(LaserData value) 
        { 
            data = value; 
            
            if (explosive != null)
                explosive.SetExplosionPrefab(value.explosion);
        }
            
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
