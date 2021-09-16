using System;
using UnityEngine;

public class WinLossCounter : MonoBehaviour
{
    public int wins;
    public int losses;
    
    public Action<int, int> onRefresh;
    
    public void Increment(bool isWin)
    {
        if (isWin) Win();
        else Lose();
    }
    
    public void Win() 
    { 
        wins++;
        Refresh();
    }
    
    public void Lose() 
    { 
        losses++; 
        Refresh();
    }
    
    public void Clear()
    {
        wins = 0;
        losses = 0;
        Refresh();
    }
    
    void Refresh() { onRefresh?.Invoke(wins, losses); }
}
