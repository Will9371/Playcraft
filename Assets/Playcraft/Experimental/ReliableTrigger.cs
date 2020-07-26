using UnityEngine;
using Playcraft.Optimized;

// NOT VERIFIED
public class ReliableTrigger : MonoBehaviour
{
    [SerializeField] ColliderEvent Enter;
    [SerializeField] ColliderEvent Exit;
    [SerializeField] int requiredTransitionFrames = 3;

    EdgeDetect edge;
    
    Collider other;
    bool inContact;
    
    void Start()
    {
        edge = new EdgeDetect(requiredTransitionFrames);
    }
    
    void OnTriggerStay(Collider other)
    {
        this.other = other;
        inContact = true;
    }
    
    void Update()
    {
        edge.Update(inContact);
        
        if (edge.changeHigh) Enter.Invoke(other);
        else if (edge.changeLow) Exit.Invoke(other);
        
        inContact = false;
    }
}
