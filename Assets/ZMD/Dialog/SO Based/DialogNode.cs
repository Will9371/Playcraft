using System;
using UnityEngine;

namespace ZMD.Dialog
{
    [CreateAssetMenu(menuName = "ZMD/Dialog/Node", fileName = "Dialog Node")]
    public class DialogNode : ScriptableObject
    {
        public string narrative;
        public ResponseOptions[] responses;
        public SO[] events;
        public OccasionInfo[] occasions;
        
        [Serializable] public struct ResponseOptions
        {
            public DialogNode node;
            public string response;
        }
    }
}
