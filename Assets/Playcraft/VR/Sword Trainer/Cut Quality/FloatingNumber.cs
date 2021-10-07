using UnityEngine;
using UnityEngine.UI;

namespace Playcraft.Examples.SwordTrainer
{
    public class FloatingNumber : MonoBehaviour
    {
        [SerializeField] Text score;
        [SerializeField] float startDistance;
        [SerializeField] float lifetime;
        [SerializeField] float fullSpeed;
        [SerializeField] AnimationCurve speedCurve;
        [Tooltip("Set Input to score range and Output to font range")]
        [SerializeField] RemapToFloat remapFontSize;
        [SerializeField] RemapToColor remapColor;
        
        float percent => (Time.time - startTime) / lifetime;
        float speed => fullSpeed * speedCurve.Evaluate(percent);
        Vector3 step => speed * Time.deltaTime * moveDirection;
        
        float startTime;
        Vector3 moveDirection;
        
        void Awake()
        {
            if (!score) score = GetComponent<Text>();
            if (!score) Debug.Log("FloatingNumber on " + gameObject + " requires a text component");
        }

        public void Begin(Transform source, Vector3 incomingDirection, float value)
        {        
            startTime = Time.time;
            
            var startingOffset = -incomingDirection * startDistance; 
            transform.Translate(startingOffset);
            moveDirection = (transform.position - source.position).normalized;
            
            score.text = value.ToString("F1");
            score.fontSize = Mathf.RoundToInt(remapFontSize.Remap(value));
            score.color = remapColor.Remap(value);
        }
        
        void Update()
        {
            transform.Translate(step, Space.World);
            if (percent > 1f) gameObject.SetActive(false);
        }
    }
}
