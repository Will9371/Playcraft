// INCOMPLETE
using System;

namespace Playcraft.Saving
{
    public class GameEvents : Singleton<GameEvents>
    {
        public static Action dispatchOnLoadEvent;
        public static SaveData current;
    }
}
