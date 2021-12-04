using UnityEngine;

public class TorqueLookTowardMono : MonoBehaviour
{
    [SerializeField] TorqueLookToward process;
    void FixedUpdate() { process.FixedUpdate(); }
}
