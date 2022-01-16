using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class SetRespondToCustomTagsArray
    {
        [SerializeField] RespondToCustomTagMono[] components;
        [SerializeField] SOArray[] tagGroups;
        
        public void SetTriggerTags(int groupIndex, int responderIndex = 0)
        {
            SetTriggerTags(tagGroups[groupIndex].values, responderIndex);
        }
        
        public void SetTriggerTags(SO[] tags, int index = 0)
        {
            foreach (var component in components)
                component.SetTriggerTags(tags, index);
        }
        
        [Serializable] public struct SOArray { public SO[] values; }
    }
}
