namespace Playcraft.Examples.SwordTrainer
{
    public enum SwordModeId 
    { 
        Inactive,
        Cut, 
        Parry, 
        CutParryAlternating,
        CutParrySimultaneous,
        DoubleCutAlternating, 
        DoubleParryAlternating,
        DoubleCutSimultaneous, 
        DoubleParrySimultaneous,
        DoubleParryCut,
    }

    public interface ISwordAction 
    { 
        void Trigger(); 
        bool hittable { get; }
    }
}
