using UnityEngine;
using Playcraft.FPS;

namespace Playcraft.Pooling
{
    public class CreateLaser : MonoBehaviour
    {
        [SerializeField] GameObject prefab;
        [SerializeField] LaserData data;
        
        ObjectPoolMaster spawner => ObjectPoolMaster.instance;
        
        public void Shoot()
        {
            //Debug.DrawLine(transform.position, transform.position + transform.forward * 3f, Color.red, 5f);
            GameObject laserObj = spawner.Spawn(prefab, transform.position);
            laserObj.transform.rotation = transform.rotation;
            
            Laser laser = laserObj.GetComponent<Laser>();
            if (laser != null && data != null) 
                laser.SetData(data);
        }
    }
}
