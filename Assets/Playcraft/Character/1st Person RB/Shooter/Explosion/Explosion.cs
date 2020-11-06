using UnityEngine;

namespace Playcraft
{
    public class Explosion : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] ExplosionForce force;
        [SerializeField] ParticleSystem[] particles;
        #pragma warning restore 0649
        
        public void Explode()
        {
            force.Explode();
            
            foreach (var particle in particles)
                particle.Play();
        }
    }
}
