using UnityEngine;
using UnityEngine.UI;

namespace Playcraft.Examples.SwordTrainer
{
    public class SwordTrainerScore : MonoBehaviour
    {
        [SerializeField] WinLossCounter cutScore;
        [SerializeField] WinLossCounter parryScore;
        [SerializeField] Text successfulCuts;
        [SerializeField] Text failedParries;
        
        void Awake()
        {
            cutScore.onRefresh += RefreshCuts;
            parryScore.onRefresh += RefreshParries;
        }
        
        void OnDestroy()
        {
            if (cutScore) cutScore.onRefresh -= RefreshCuts;
            if (parryScore) parryScore.onRefresh -= RefreshParries;
        }
        
        void RefreshCuts(int wins, int losses) { successfulCuts.text = "Target Damage: " + wins; }
        
        void RefreshParries(int wins, int losses) { failedParries.text = "Player Damage: " + losses; }
    }
}