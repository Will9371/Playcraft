using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class UpdateTargetSettings : MonoBehaviour
    {
        [SerializeField] FightModeSequence sequence;
        [SerializeField] Binding[] bindings;
    
        void Update()
        {
            foreach (var binding in bindings)
                if (Input.GetKeyDown(binding.key))
                    sequence.ChangeTargetSettings(binding.cutSettings, binding.parrySettings);
        }
        
        [Serializable]
        public class Binding
        {
            public KeyCode key;
            public CutTargetSettings[] cutSettings;
            public ParryTargetSettings[] parrySettings;
        }
    }
}
