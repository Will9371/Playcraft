using UnityEngine;

namespace Playcraft
{
    public class CircleTargetMono : MonoBehaviour
    {
        [SerializeField] CircleTarget process;
        
        [SerializeField] Vector2 weightRange = new Vector2(0.5f, 1.5f);
        [SerializeField] bool randomizeOnEnable;
        [SerializeField] Vector3Event Output;
        
        float weight;


        void Start() { if (!process.self) process.self = transform; }

        void Update() { Output.Invoke(process.Update() * weight); }
        
        void OnEnable()
        {
            if (randomizeOnEnable)
            {
                RandomizeDirection();
                RandomizeWeight();
            }
        }
        
        public void SetTarget(Transform value) { process.target = value; }

        public void RandomizeDirection() { process.SetRandomDirection(); }
        
        public void RandomizeWeight() { weight = Random.Range(weightRange.x, weightRange.y); }
    }
}
