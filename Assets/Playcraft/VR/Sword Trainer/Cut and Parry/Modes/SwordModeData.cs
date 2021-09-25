using System;

namespace Playcraft.Examples.SwordTrainer
{
    [Serializable] 
    public class SwordModeData
    {
        public int cutsActive;
        public int parriesActive;
        //public bool doubleCutsActive;
        //public bool doubleParriesActive;
            
        [NonSerialized]
        public ISwordMode process;
                
        public void Initialize(SwordModeId id, SwordTrainer controller)
        {
            switch (id)
            {
                case SwordModeId.Inactive: process = null; break;
                case SwordModeId.Cut: process = new CutOnlyMode(controller); break;
                case SwordModeId.Parry: process = new ParryOnlyMode(controller); break;
                case SwordModeId.CutParryAlternating: process = new CutAndParryMode(controller); break;
                case SwordModeId.DoubleCutAlternating: process = new DoubleCutAlternatingMode(controller); break;
                case SwordModeId.DoubleCutSimultaneous: process = new DoubleCutSimultaneousMode(controller); break;
                case SwordModeId.CutParrySimultaneous: process = new CutAndParrySimultaneousMode(controller); break;
                case SwordModeId.DoubleParryAlternating: process = new DoubleParryAlternatingMode(controller); break;
                case SwordModeId.DoubleParrySimultaneous: process = new DoubleParrySimultaneousMode(controller); break;
            }
        }
    }
}