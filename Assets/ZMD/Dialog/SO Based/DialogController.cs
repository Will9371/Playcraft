using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD.Dialog
{
    public class DialogController : MonoBehaviour
    {
        [Serializable] class DialogNodeEvent : UnityEvent<DialogNode> { }
    
        [SerializeField] DialogNode node;
        [SerializeField] ResponseButtons response;
        [SerializeField] TagEvent RelayEvent;
        [SerializeField] DialogNodeEvent RelayNode;
        
        void Start()
        {
            RelayNode.Invoke(node);
            response.onClick = Transition;
        }
        
        public void Transition(int index)
        {
            node = node.responses[index].node;
            RelayNode.Invoke(node);
            
            foreach (var item in node.events)
                RelayEvent.Invoke(item);
        }
        
        public void DisplayOptions() => response.Refresh(node);
    }
}
