using UnityEngine;

namespace Playcraft
{
    public class FilterMessageLink : MonoBehaviour
    {
        [SerializeField] MessageLinkEvent Output = default;
        
        public void Input(Collider value)
        {
            var link = value.GetComponent<MessageLink>();
            if (link) Output.Invoke(link);
        }
    }
}
