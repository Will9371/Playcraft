using System.Collections.Generic;
using UnityEngine;

// FUTURE USE
namespace Playcraft
{
    public class InteractableList : MonoBehaviour
    {
        [SerializeField] IMessageEvent OnAdd;
        [SerializeField] IMessageEvent OnRemove;

        List<IMessage> values = new List<IMessage>();

        public int count => values.Count;
        public bool hasAny => count > 0;
        public bool isEmpty => count == 0;

        public void Add(Collider value) { Add(value.GetComponent<IMessage>()); }
        public void Add(IMessage value)
        {
            if (value == null) return;
            values.Add(value);
            OnAdd.Invoke(value);
        }

        public void Remove(Collider value) { Remove(value.GetComponent<IMessage>()); }
        public void Remove(IMessage value)
        {
            if (value == null) return;
            values.Remove(value);
            OnRemove.Invoke(value);
        }

        public void Clear() 
        {
            for (int i = values.Count - 1; i >= 0; i--)
                Remove(values[i]);

            values.Clear();
        }

        public void MessageAll(SO message)
        {
            foreach (var value in values)
                value.Message(message);
        }

        public void MessageIndex(SO message, int index = 0)
        {
            if (count <= index) return;
            values[index].Message(message);
        }
    }
}
