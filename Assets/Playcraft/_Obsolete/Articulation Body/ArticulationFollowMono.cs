using UnityEngine;

public class ArticulationFollowMono : MonoBehaviour
{
    public ArticulationFollow process;
    void FixedUpdate() { process.FixedUpdate(); }
}
