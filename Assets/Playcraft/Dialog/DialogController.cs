using System;
using UnityEngine;
using UnityEngine.UI;

// Consider refactor this & DialogNode class to separate state machine from text display
namespace Playcraft
{
    public class DialogController : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Text narrative;
        [SerializeField] ResponseOption[] responses;
        [SerializeField] DialogNode node;
        [SerializeField] TagEvent RelayEvent;
        #pragma warning restore 0649
        
        private void Start()
        {
            Display();
        }
        
        private void Display()
        {
            narrative.text = node.narrative;
            var nodeResponseCount = node.responses.Length;
            
            for (int i = 0; i < responses.Length; i++)
            {
                var withinList = i < nodeResponseCount;
            
                if (withinList)
                    responses[i].text.text = node.responses[i].response;
                    
                responses[i].button.SetActive(withinList);
            }
        }
        
        public void Transition(int index)
        {
            node = node.responses[index].node;
            Display();
            
            foreach (var item in node.events)
                RelayEvent.Invoke(item);
        }
        
        [Serializable] struct ResponseOption
        {
            #pragma warning disable 0649
            public GameObject button;
            public Text text;
            #pragma warning restore 0649
        }
    }
}
