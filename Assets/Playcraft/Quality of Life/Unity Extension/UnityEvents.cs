using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Playcraft;

[Serializable] public class Vector3Event : UnityEvent<Vector3> { }
[Serializable] public class Vector3x2Event : UnityEvent<Vector3, Vector3> { }
[Serializable] public class Vector3ArrayEvent : UnityEvent<Vector3[]> { }
[Serializable] public class Vector2Event : UnityEvent<Vector2> { }
[Serializable] public class BoolEvent : UnityEvent<bool> { }
[Serializable] public class FloatEvent : UnityEvent<float> { }
[Serializable] public class FloatArrayEvent : UnityEvent<float[]> { }
[Serializable] public class IntEvent : UnityEvent<int> { }
[Serializable] public class StringEvent : UnityEvent<string> { }
[Serializable] public class GameObjectEvent : UnityEvent<GameObject> { }
[Serializable] public class TransformEvent : UnityEvent<Transform> { }
[Serializable] public class ColliderEvent : UnityEvent<Collider> { }
[Serializable] public class CollisionEvent : UnityEvent<Collision> { }
[Serializable] public class RaycastHitEvent : UnityEvent<RaycastHit> { }
[Serializable] public class TransformListEvent : UnityEvent<List<Transform>> { }
[Serializable] public class GameObjectBoolEvent : UnityEvent<GameObject, bool> { }
[Serializable] public class TagEvent : UnityEvent<SO> { }
[Serializable] public class GameObjectTagEvent : UnityEvent<SO, GameObject> { }
[Serializable] public class GameObjectVector3Event : UnityEvent<GameObject, Vector3> { }
[Serializable] public class ColorEvent : UnityEvent<Color> { }
[Serializable] public class Collider2DEvent : UnityEvent<Collider2D> { }

[Serializable] public class IMessageEvent : UnityEvent<IMessage> { }

