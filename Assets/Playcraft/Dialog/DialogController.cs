using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playcraft
{
    public class DialogController : MonoBehaviour
    {
        [Serializable] class DialogNodeEvent : UnityEvent<DialogNode> { }
    
        #pragma warning disable 0649
        [SerializeField] DialogNode node;
        [SerializeField] ResponseOption[] responses;
        [SerializeField] TagEvent RelayEvent;
        [SerializeField] DialogNodeEvent RelayNode;
        #pragma warning restore 0649
        
        private void Start()
        {
            RelayNode.Invoke(node);
        }
        
        public void Transition(int index)
        {
            node = node.responses[index].node;
            RelayNode.Invoke(node);
            
            foreach (var item in node.events)
                RelayEvent.Invoke(item);
        }
        
        // REFACTOR: move to buttons
        public void DisplayOptions()
        {
            var nodeResponseCount = node.responses.Length;
        
            for (int i = 0; i < responses.Length; i++)
            {
                var withinList = i < nodeResponseCount;
            
                if (withinList)
                    responses[i].text.text = node.responses[i].response;
                    
                responses[i].button.SetActive(withinList);
            }
        }
    }
    
    // REFACTOR: move to buttons
    [Serializable] struct ResponseOption
    {
        #pragma warning disable 0649
        public GameObject button;
        public Text text;
        #pragma warning restore 0649
    }
}
