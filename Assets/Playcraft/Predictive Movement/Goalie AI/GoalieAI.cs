using UnityEngine;
using Playcraft;

public class GoalieAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform center;
    [SerializeField] float radius = 40f;
    [SerializeField] float speed = 10f;
    
    FollowOnCircle bounds = new FollowOnCircle();
    
    void OnValidate()
    {
        bounds.radius = radius;
    }
    
    void Update()
    {
        bounds.center = center.position;
        transform.position = Vector3.MoveTowards(transform.position, bounds.Update(target.position), speed * Time.deltaTime);
    }
}
