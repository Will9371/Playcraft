using UnityEngine;
using ZMD.FPS;
using ZMD.Pooling;

public class Gun : MonoBehaviour
{
    [SerializeField] LaserData laserData;
    [SerializeField] CreateLaser muzzle;
    
    public void SetLaserData(LaserData value) { laserData = value; }
    public void Shoot() { muzzle.Shoot(laserData); }
}
