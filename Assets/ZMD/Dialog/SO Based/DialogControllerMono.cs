using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD.Dialog
{
    [Serializable] class DialogNodeEvent : UnityEvent<DialogNode> { }

    public class DialogControllerMono : MonoBehaviour
    {
        public void Begin(DialogNode startingNode) => process.Begin(startingNode);
        public void DisplayOptions() => process.DisplayOptions();
        public DialogController process;
        
        void Start() => process.onSetNode = OnSetNode;
        void OnSetNode(DialogNode value) => RelayNode.Invoke(value);

        [SerializeField] DialogNodeEvent RelayNode;
    }
    
    [Serializable]
    public class DialogController
    {
        NarrativeHub narrativeHub => NarrativeHub.instance;
    
        [SerializeField] GameObject display;
        [SerializeField] SO ending;
        [SerializeField] ResponseButtons response;
        
        DialogNode _node;
        DialogNode node
        {
            get => _node;
            set
            {
                _node = value;
                onSetNode?.Invoke(value);
            }
        }
        public Action<DialogNode> onSetNode;
        
        bool inProgress;

        public void Begin(DialogNode startingNode)
        {
            if (inProgress) return;
            
            Initialize();
            display.SetActive(true);
            node = startingNode;
            
            inProgress = true;
        }

        bool initialized;
        void Initialize()
        {
            if (initialized) return;
            response.onClick = Transition;
            initialized = true;
        }
        
        public Action<SO> onTriggerEvent;
        
        void Transition(int index)
        {
            node = node.responses[index].node;
            
            foreach (var item in node.events)
            {
                if (item == ending) 
                {
                    display.SetActive(false);
                    inProgress = false;
                    narrativeHub.systemRefresh?.Invoke();
                }
                else onTriggerEvent?.Invoke(item);
            }
        }
        
        public void DisplayOptions() => response.Refresh(node);
    }
}
