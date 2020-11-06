using UnityEngine;

[CreateAssetMenu(menuName = "Playcraft/Scene Management/Transition")]
public class SceneTransitionSO : ScriptableObject
{
    public string displayMessage;
    public float minimumLoadScreenTime = 2f;
    public SceneSO[] scenesToLoad;
    public SceneSO[] scenesToUnload;
}
