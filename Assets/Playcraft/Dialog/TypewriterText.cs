using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playcraft
{
    public class TypewriterText : MonoBehaviour
    {
        [SerializeField] Text text;
        [SerializeField] UnityEvent OnComplete;
        [Tooltip("In characters per second")]
        [SerializeField] float speed = 10;

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
