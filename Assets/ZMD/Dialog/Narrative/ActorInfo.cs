using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZMD.Dialog
{
    [CreateAssetMenu(menuName = "ZMD/Narrative/Actor")]
    public class ActorInfo : ScriptableObject
    {
        public string displayName;
        
        [Tooltip("Dialog trees, unlockable based on relationship to player")]
        public List<Narrative> narratives;
        
        [Tooltip("Extent to which others' opinions of self affect own opinions of others")]
        public RelationshipParameters mirroring;
        
        [Tooltip("Extent to which others' opinions of others affect own opinions of others")]
        public RelationshipParameters empathy;
    }

    [Serializable]
    public class Narrative
    {
        public Relationship requirement;
        public DialogNode startingNode;
        public bool IsAvailable(Relationship relationship) => requirement.MeetsThresholds(relationship);
        public bool onceOnly;
    }
}