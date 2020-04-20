using UnityEngine;

[CreateAssetMenu(menuName = "Character/Move State")]
public class MoveState : ScriptableObject
{
    public float moveSpeed;
    public AnimatedMoveState animations;
}
