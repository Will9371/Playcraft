using UnityEngine;
using Playcraft.FPS;
using Playcraft.Pooling;

public class Gun : MonoBehaviour
{
    [SerializeField] LaserData laserData;
    [SerializeField] CreateLaser muzzle;
    
    public void SetLaserData(LaserData value) { laserData = value; }
    public void Shoot() { muzzle.Shoot(laserData); }
}
