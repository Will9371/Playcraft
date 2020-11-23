using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class ExpandingSphere : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform objectToScale;
        [SerializeField] float growthSpeed = 2;
        [SerializeField] float minScale = .2f;
        [SerializeField] float maxScale = .75f;
        [SerializeField] float delayResolution = .01f;
        [SerializeField] UnityEvent Activate;
        [SerializeField] UnityEvent Deactivate;
        #pragma warning restore 0649
        
        float scale => objectToScale.localScale.x; 
        
        Vector3 startScale;
        float growStep;
        bool active;
        
        void Start()
        {
            startScale = new Vector3(minScale, minScale, minScale);
            growStep = growthSpeed * delayResolution;
        }

        public void Begin()
        {
            active = true;
            objectToScale.localScale = startScale;
            StartCoroutine(Grow());
            Activate.Invoke();
        }
        
        public void End()
        {
            active = false;
            objectToScale.localScale = startScale;
            Deactivate.Invoke();
        }

        float newScale;

        IEnumerator Grow()
        {
            var delay = new WaitForSeconds(delayResolution);
            
            while (active)
            {
                if (scale < maxScale)
                {
                    newScale = scale + growStep;
                    objectToScale.localScale = new Vector3(newScale, newScale, newScale);
                }

                yield return delay;
            }        
        }
    }
}
