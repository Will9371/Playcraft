using UnityEngine;
using Playcraft.Examples.SwordTrainer;

public class FightModeSequence : MonoBehaviour
{
    [SerializeField] SwordModeId[] sequence;
    [SerializeField] SwordTrainer fighter;
    
    int index;
    
    public void Cycle()
    {
        index++;
        
        if (index >= sequence.Length)
            index = 0;
            
        fighter.SetMode(sequence[index]);
    }
}