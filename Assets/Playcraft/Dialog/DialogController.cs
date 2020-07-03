using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playcraft
{
    public class DialogController : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Text narrative;
        [SerializeField] ResponseOption[] responses;
        [SerializeField] DialogNode node;
        [SerializeField] UnityEvent OnDialogEnd;
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
            
            if (nodeResponseCount <= 0)
                OnDialogEnd.Invoke(); 
        }
        
        public void Transition(int index)
        {
            node = node.responses[index].node;
            Display();
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
