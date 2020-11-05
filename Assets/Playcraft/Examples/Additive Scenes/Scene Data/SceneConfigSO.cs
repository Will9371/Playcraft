﻿using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Playcraft/Scene Management/Scene Config List")]
public class SceneConfigSO : ScriptableObject
{
    public SceneSO[] universalScenes;
    public SceneConfig[] sceneConfigurations;
}

[Serializable] public class SceneConfig
{
    public bool isDefaultStartingScene;
    public SceneSO uniqueScene;
    public SceneSO[] supportingScenes;
}