using System.Collections;
using UnityEngine;

public class ExpandingSphere : MonoBehaviour
{
    [SerializeField] Transform objectToScale;
    
    [SerializeField] float growthSpeed = 2;
    [SerializeField] float minScale = .2f;
    [SerializeField] float maxScale = .75f;
    [SerializeField] float delayResolution = .01f;
    
    float scale { get { return objectToScale.localScale.x; } }
    
    Vector3 startScale;
    float growStep;
    bool active;
    
    private void Start()
    {
        startScale = new Vector3(minScale, minScale, minScale);
        growStep = growthSpeed * delayResolution;
    }

    public void Begin()
    {
        objectToScale.localScale = startScale;
        active = true;
        StartCoroutine(Grow());
    }
    
    public void End()
    {
        active = false;
    }

    private IEnumerator Grow()
    {    
        while (active)
        {
            if (scale < maxScale)
            {
                var newScale = scale + growStep;
                objectToScale.localScale = new Vector3(newScale, newScale, newScale);
            }

            yield return new WaitForSeconds(delayResolution);
        }
        
        //objectToScale.localScale = startScale;
    }
}
