// Requires Json.Net for Unity

/*
using UnityEngine;

namespace Playcraft.Saving
{
    [CreateAssetMenu(menuName = "Rogue Invader/Conditions/Flag Set Condition", fileName = "Flag Set Condition")]
    public class FlagSetCondition : Condition
    {
        Profile profile => ProfileManager.instance.activeProfile;

        public StringSO flagId;

        public override bool IsConditonMet()
        {
            if (profile == null) return false;
            return profile.savedProperties.GetBool(flagId.value);
        }
    }
}
*/