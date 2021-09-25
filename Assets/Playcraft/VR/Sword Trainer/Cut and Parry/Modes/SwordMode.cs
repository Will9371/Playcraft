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
        DoubleParrySimultaneous
    }

    public interface ISwordMode
    {
        void Enter();
        void Exit();
        void CutComplete(int index);
        void ParryComplete(int index);
    }
}
