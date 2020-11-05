using UnityEngine;

[CreateAssetMenu(menuName = "Playcraft/Scene Management/Transition")]
public class SceneTransitionSO : ScriptableObject
{
    public SceneSO[] scenesToLoad;
    public SceneSO[] scenesToUnload;
}
