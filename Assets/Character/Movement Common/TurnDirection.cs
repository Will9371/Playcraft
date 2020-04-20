using UnityEngine;

[CreateAssetMenu(menuName = "Character/Turn Direction")]
public class TurnDirection : ScriptableObject
{
    public Axis axis;
    public bool clockwise;
}
