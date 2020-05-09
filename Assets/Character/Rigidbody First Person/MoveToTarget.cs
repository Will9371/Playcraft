using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target;
    public float speed;
    
    Vector3 targetVector { get { return target.position - transform.position; } }
    Vector3 targetDirection { get { return targetVector.normalized; } }
    Vector3 step { get { return targetDirection * speed * Time.deltaTime; } }
    float targetDistance { get { return Vector3.Distance(target.position, transform.position); } }

    void Update()
    {
        if (targetDistance > step.magnitude)
            transform.Translate(step);
        else
            transform.position = target.position;
    }
}
