using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public enum SwordModeId 
    { 
        Inactive,
        Cut, 
        Parry, 
        CutAndParry,
        CutAndParryAlternating,
        CutAndParrySimultaneous,
        DoubleCutAlternating, 
        DoubleParryAlternating,
        DoubleCutSimultaneous, 
        DoubleParrySimultaneous
    }

    public interface ISwordMode
    {
        void Enter();
        void Exit();
        void CutComplete();
        void ParryComplete();
    }
    
    [CreateAssetMenu(menuName = "Playcraft/VR/Sword Trainer/Action Mode")]
    public class SwordMode : ScriptableObject
    {
        public bool cutsActive;
        public bool parriesActive;
    }
}
