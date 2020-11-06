using UnityEngine;

[CreateAssetMenu(menuName = "Playcraft/Scene Management/Transition")]
public class SceneTransitionSO : ScriptableObject
{
    public string displayMessage;
    public float minimumLoadScreenTime = 2f;
    public StringSO[] scenesToLoad;
    public StringSO[] scenesToUnload;
}
