using System;
using UnityEngine.Events;

namespace ZMD
{
    [Serializable] public class TrinaryEvent : UnityEvent<Trinary> { }
    public enum Trinary { True, False, Unknown }
}
