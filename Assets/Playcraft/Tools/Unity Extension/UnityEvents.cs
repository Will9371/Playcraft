﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class Vector3Event : UnityEvent<Vector3> { }
[Serializable] public class Vector2Event : UnityEvent<Vector2> { }
[Serializable] public class FloatEvent : UnityEvent<float> { }
[Serializable] public class IntEvent : UnityEvent<int> { }
[Serializable] public class StringEvent : UnityEvent<string> { }
[Serializable] public class TransformEvent : UnityEvent<Transform> { }
[Serializable] public class ColliderEvent : UnityEvent<Collider> { }
[Serializable] public class CollisionEvent : UnityEvent<Collision> { }

[Serializable] public class TransformListEvent : UnityEvent<List<Transform>> { }