using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRotation : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] NonRigidbodyMovement movement;

    bool rotating = false;
    private void Update()
    {
        //Rotate player and camera
        if(!rotating)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                StartCoroutine(RotateWorld(true));
            else if (Input.GetKeyDown(KeyCode.E))
                StartCoroutine(RotateWorld(false));
        }
    }

    IEnumerator RotateWorld(bool ccw)
    {
        rotating = true;

        Vector3 oldGravity = movement.gravity;
        movement.gravity = Vector3.zero;

        Quaternion oldRotation = player.transform.rotation;
        Quaternion newRotation = GetNewRotation(oldGravity, ccw);

        float currentCamRotation = transform.localEulerAngles.z;
        float targetCamRotation = currentCamRotation + 120 * (ccw ? -1 : 1);
        float timer = 0;
        float tweenTime = 1;
        while(timer < tweenTime)
        {
            timer += Time.deltaTime;
            if (timer > tweenTime)
                timer = tweenTime;

            this.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Mathf.Lerp(currentCamRotation, targetCamRotation, timer / tweenTime));
            player.transform.rotation = Quaternion.Lerp(oldRotation, newRotation, timer / tweenTime);
            yield return null;
        }

        movement.gravity = GetNewGravity(oldGravity, ccw);
        rotating = false;
    }

    Vector3 GetNewGravity(Vector3 oldGrav, bool ccw)
    {
        if (Vector3.Dot(Vector3.down, oldGrav) > 0.1f) //-y gravity
            return ccw ? Vector3.forward : Vector3.right;
        else if (Vector3.Dot(Vector3.right, oldGrav) > 0.1f) //+x gravity
            return ccw ? Vector3.down : Vector3.forward;
        else //+z gravity
            return ccw ? Vector3.right : Vector3.down;
    }

    Quaternion GetNewRotation(Vector3 oldGrav, bool ccw)
    {
        if (Vector3.Dot(Vector3.down, oldGrav) > 0.1f) //-y gravity
            return ccw ? Quaternion.Euler(0, 90, -90) : Quaternion.Euler(90, 0, 90);
        else if (Vector3.Dot(Vector3.right, oldGrav) > 0.1f) //+x gravity
            return ccw ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(-0, 90, -90);
        else //+z gravity
            return ccw ? Quaternion.Euler(90, 0, 90) : Quaternion.Euler(0, 0, 0);
    }
}
