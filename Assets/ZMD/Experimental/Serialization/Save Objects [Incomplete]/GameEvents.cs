// INCOMPLETE
using System;

namespace ZMD.Saving
{
    public class GameEvents : Singleton<GameEvents>
    {
        public static Action dispatchOnLoadEvent;
        public static SaveData current;
    }
}
