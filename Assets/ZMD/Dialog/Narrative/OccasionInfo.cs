using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ZMD/Dialog/Occasion")]
public class OccasionInfo : ScriptableObject
{
    public Action onTrigger;
    public void Trigger() => onTrigger?.Invoke(); 
}
