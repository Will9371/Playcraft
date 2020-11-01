using UnityEngine;

namespace Playcraft
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] ExplosionForce force;
        [SerializeField] ParticleSystem[] particles;
        
        public void Explode()
        {
            force.Explode();
            
            foreach (var particle in particles)
                particle.Play();
        }
    }
}
