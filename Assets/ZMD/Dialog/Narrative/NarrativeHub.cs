using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZMD.Dialog
{
    public class NarrativeHub : Singleton<NarrativeHub>
    {
        public ActorInfo player;
        public DialogControllerMono dialog;
        
        public Action systemRefresh;
        
        public List<ActorBinding> actors;
        
        void Start()
        {
            Invoke(nameof(SystemRefresh), .01f);
        }
        
        void SystemRefresh() => systemRefresh?.Invoke();
        
        public ActorMono GetActor(ActorInfo id)
        {
            foreach (var actor in actors)
                if (actor.id == id)
                    return actor.person;
                    
            Debug.LogError($"Invalid actor {id}");
            return null;
        }
        
        [Serializable]
        public class ActorBinding
        {
            public ActorInfo id;
            public ActorMono person;
        }
    }
}
