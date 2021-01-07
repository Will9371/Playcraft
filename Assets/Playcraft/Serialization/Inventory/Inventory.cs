using Playcraft.Pooling;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Examples.Saving
{
    public class Inventory : MonoBehaviour
    {
        //[SerializeField] SpawnPrefab sphereSpawner;
        //[SerializeField] SpawnPrefab cubeSpawner;
        [SerializeField] int sphereCost = 200;
        [SerializeField] int cubeCost = 300;
        [SerializeField] UnityEvent OnUpdate;

        SaveData saveData => SaveData.current;
        PlayerProfile profile => SaveData.current.profile;

        public void AddCoins(int value)
        {
            profile.currency += value;
            profile.experience += 1;
            OnUpdate.Invoke();
        }
        
        public void BuySphere()
        {
            if (profile.currency < sphereCost) return;
            profile.currency -= sphereCost;
            //sphereSpawner.Spawn();
            saveData.sphereCount += 1;
            OnUpdate.Invoke();
        }
        
        public void BuyCube()
        {
            if (profile.currency < cubeCost) return;
            profile.currency -= cubeCost;
            //cubeSpawner.Spawn();
            saveData.cubeCount += 1;
            OnUpdate.Invoke();
        }
        
        public void Reset()
        {
            profile.currency = 0;
            profile.experience = 0;
            saveData.cubeCount = 0;
            saveData.sphereCount = 0;
            
            SerializationManager.Save();
            OnUpdate.Invoke();
        }
    }
}
