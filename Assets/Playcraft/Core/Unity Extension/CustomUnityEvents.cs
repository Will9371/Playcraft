using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    [Serializable] public class IMessageEvent : UnityEvent<IMessage> { }
    [Serializable] public class SOEvent : UnityEvent<ScriptableObject> { }
    [Serializable] public class SOListEvent : UnityEvent<List<ScriptableObject>> { }
    [Serializable] public class SOFloatEvent : UnityEvent<SO, float> { }
    [Serializable] public class TagEvent : UnityEvent<SO> { }
    [Serializable] public class GameObjectTagEvent : UnityEvent<SO, GameObject> { }
}