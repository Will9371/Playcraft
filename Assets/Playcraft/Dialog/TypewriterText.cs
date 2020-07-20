using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playcraft.Dialog
{
    public class TypewriterText : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Text text;
        [SerializeField] UnityEvent OnComplete;
        [Tooltip("In characters per second")]
        [SerializeField] float speed = 10;
        #pragma warning restore 0649

        public void Input(string value)
        {
            StartCoroutine(TypeText(value));
        }
        
        IEnumerator TypeText(string value)
        {
            text.text = "";

            foreach (var character in value)
            {
                text.text += character;
                yield return new WaitForSeconds(1/speed);
            }
            
            OnComplete.Invoke();
        }
    }
}
