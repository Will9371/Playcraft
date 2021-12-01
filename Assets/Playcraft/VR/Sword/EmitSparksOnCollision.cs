using UnityEngine;
using Playcraft;

public class EmitSparksOnCollision : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;
    [SerializeField] ExtendedCurve impulseMultiplier;
    [SerializeField] float minCutoff;
    
    ParticleSystem.MainModule main;
    
    void Awake()
    {
        main = particles.main;
    }
    
    void OnCollisionEnter(Collision other)
    {
        var force = other.impulse.magnitude;
        if (force < minCutoff) return;

        particles.transform.position = other.contacts[0].point;
        main.startSizeMultiplier = impulseMultiplier.Evaluate(force);
        particles.Play();
    }
}
