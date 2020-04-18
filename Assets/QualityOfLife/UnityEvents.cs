using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class Vector3Event : UnityEvent<Vector3> { }
[Serializable] public class Vector2Event : UnityEvent<Vector2> { }
[Serializable] public class FloatEvent : UnityEvent<float> { }
[Serializable] public class IntEvent : UnityEvent<int> { }
[Serializable] public class StringEvent : UnityEvent<string> { }