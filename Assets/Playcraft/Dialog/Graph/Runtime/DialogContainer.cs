// Credit: Mert Kirimgeri
// Source: https://www.youtube.com/watch?v=OMDfr1dzBco

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft.Experimental.DialogGraph
{
    [Serializable]
    public class DialogContainer : ScriptableObject
    {
        public List<NodeLinkData> nodeLinks = new List<NodeLinkData>();
        public List<DialogNodeData> dialogNodeData = new List<DialogNodeData>();
    }
}
