using System.Collections;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class ArcParry : MonoBehaviour
    {
        enum HitState { Swing, Hurt, Parried }
        HitState hitState;
    
        [SerializeField] LerpAxisAngle zPivot;
        [SerializeField] float zPivotDuration;
        [SerializeField] LerpAxisAngle xPivot;
        [SerializeField] float xPivotDuration;
        [SerializeField] GameObject blade;
        [SerializeField] GameObject trail;
        [SerializeField] Color hurtColor = Color.red;
        [SerializeField] Color defaultColor = Color.yellow;
        [SerializeField] Color parriedColor = Color.white * .15f;
        [SerializeField] float restingX = -30f;
        
        Renderer bladeRenderer;
        TrailRenderer trailRenderer;
        GetPercentOverTime timer = new GetPercentOverTime();

        void Awake()
        {
            bladeRenderer = blade.GetComponent<Renderer>();
            trailRenderer = trail.GetComponent<TrailRenderer>();
        }
        
        void OnValidate()
        {
            zPivot.Validate();
            xPivot.Validate();
        }
        
        void Start() 
        { 
            xPivot.SetAngle(restingX);
            StartCoroutine(Routine()); 
        }
        
        IEnumerator Routine()
        {
            while (true)
            {
                yield return SwingZ();
                yield return SwingX();
            }
        }
        
        IEnumerator SwingZ()
        {
            zPivot.SetDestination(Random.Range(0, 360));
            yield return StartCoroutine(Swing(zPivot, zPivotDuration, Vector3.zero));
        }
        
        IEnumerator SwingX()
        {
            xPivot.SetEnds(restingX, restingX + 360);
            yield return StartCoroutine(Swing(xPivot, xPivotDuration, new Vector3(0, 90, 0)));
            SetState(HitState.Swing);          
        }
        
        IEnumerator Swing(LerpAxisAngle pivot, float duration, Vector3 trailRotation)
        {
            trail.transform.localEulerAngles = trailRotation;
            yield return timer.Run(pivot, duration);
        }
        
        public void Hurt() { SetState(HitState.Hurt); }
        public void Parried() { SetState(HitState.Parried); }
        
        void SetState(HitState value)
        {
            if (hitState == value) return;
            hitState = value;
            
            switch (hitState)
            {
                case HitState.Swing: SetColor(defaultColor); break;
                case HitState.Hurt: SetColor(hurtColor); break;
                case HitState.Parried: SetColor(parriedColor); break;
            }
        }
        
        void SetColor(Color color)
        {
            trailRenderer.startColor = color;
            trailRenderer.endColor = color;
            bladeRenderer.material.color = color;
        }
    }
}
