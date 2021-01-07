using UnityEngine;
using UnityEngine.UI;

namespace Playcraft.Examples.Saving
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] Text currency;
        [SerializeField] Text experience;
        [SerializeField] Text spheres;
        [SerializeField] Text cubes;
        
        SaveData saveData => SaveData.current;
        PlayerProfile profile => saveData.profile;
        
        public void Refresh()
        {
            currency.text = profile.currency.ToString();
            experience.text = profile.experience.ToString();
            spheres.text = saveData.sphereCount.ToString();
            cubes.text = saveData.cubeCount.ToString();
        }
    }
}
