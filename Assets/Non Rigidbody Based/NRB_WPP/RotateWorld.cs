using System.Collections;
using UnityEngine;
using ZMD;

public class RotateWorld : MonoBehaviour
{
    [SerializeField] GameObject view;
    [SerializeField] GameObject player;
    [HideInInspector] public float rotationTime;

    LerpPivotZ lerpZ;
    LerpRotationIndexMono lerpRotation;
    CycleGravity cycleGravity;
    KeyboardInput keyboardInput;
    
    void Awake()
    {
        lerpZ = view.GetComponent<LerpPivotZ>();
        lerpRotation = player.GetComponent<LerpRotationIndexMono>();
        cycleGravity = player.GetComponent<CycleGravity>();
        keyboardInput = view.GetComponent<KeyboardInput>();
    }

    public void Rotate(bool clockwise)
    {
        lerpZ.SetTargetRotation(clockwise);
        lerpRotation.CycleDestination(clockwise);
        cycleGravity.Cycle(clockwise);
        
        StartCoroutine(RotateRoutine(clockwise));
    }
    
    IEnumerator RotateRoutine(bool clockwise)
    {
        keyboardInput.enabled = false;
        cycleGravity.SetActive(false);
    
        float timer = 0;

        while(timer < rotationTime)
        {
            timer += Time.deltaTime;
            if (timer > rotationTime)
                timer = rotationTime;
                
            var percent = timer / rotationTime;
            lerpZ.SetRotation(percent);
            lerpRotation.Input(percent);
                
            yield return null;
        }
        
        keyboardInput.enabled = true;
        cycleGravity.SetActive(true);
        yield return null;
    }
}
