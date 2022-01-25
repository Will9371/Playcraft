using UnityEngine;

namespace ZMD.PredictiveMovement
{
    public class GoalieAIMono : MonoBehaviour
    {
        [SerializeField] GoalieAI process;
        void OnValidate() { process.self = transform; }
        void Start() { process.Initialize(); }
        void FixedUpdate() { process.FixedUpdate(); }
    }
}