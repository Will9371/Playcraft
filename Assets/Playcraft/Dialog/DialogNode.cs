using System;
using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Dialog/Node", fileName = "Dialog Node")]
    public class DialogNode : ScriptableObject
    {
        public string narrative;
        public ResponseOptions[] responses;
        public SO[] events;
        
        [Serializable] public struct ResponseOptions
        {
            public DialogNode node;
            public string response;
        }
    }
}
