// Credit: Mert Kirimgeri
// Source: https://www.youtube.com/watch?v=7KHGH0fPL84

using UnityEditor.Experimental.GraphView;

namespace Playcraft.Experimental.DialogGraph
{
    public class DialogNode : Node
    {
        public string guid;
        public string dialogText;
        public bool entryPoint;
    }
}
