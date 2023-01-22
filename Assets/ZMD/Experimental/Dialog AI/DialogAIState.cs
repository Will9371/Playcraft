using UnityEngine;

//[CreateAssetMenu(menuName = "ZMD/Dialog/AI/State")]
public class DialogAIState : ScriptableObject
{
    public string statement;
    public DialogAIState[] prerequisites;
}
