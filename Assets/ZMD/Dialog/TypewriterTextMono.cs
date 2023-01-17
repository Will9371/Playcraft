using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace ZMD
{
    public class TypewriterTextMono : MonoBehaviour
    {
        public TypeWriterText process;
        public void Input(string value) => process.Input(this, value);
        
        void Start() => process.onComplete = OnComplete;
        void OnComplete() => onComplete.Invoke();
        [SerializeField] UnityEvent onComplete;
    }
    
    [Serializable]
    public class TypeWriterText
    {
        [SerializeField] TMP_Text text;
        [Tooltip("In characters per second")]
        [SerializeField] float speed = 10;
        
        public Action onComplete;

        public void Input(MonoBehaviour mono, string value) => mono.StartCoroutine(TypeText(value));
        IEnumerator TypeText(string value)
        {
            text.text = "";

            foreach (var character in value)
            {
                text.text += character;
                yield return new WaitForSeconds(1/speed);
            }
            
            onComplete.Invoke();
        }        
    }
}
