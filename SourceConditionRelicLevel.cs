using System.Linq;
using EffectSourceConditions;

namespace RelicKeeper
{

    public class SourceConditionRelicLevel : SourceCondition
    {
        /// <summary>
        /// The level of the relic that the condition is for. 0 means anyone can use it, 1 means you need relic lore, and 2 means you need mythic lore
        /// </summary>
        public int relicLevel;

        public bool Inverted;

        /// <summary>
        /// Returns true if RequiredSkillID <= 0 or if the character knows the Skill with ItemID = RequiredSkillID
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public override bool CharacterHasRequirement(Character character)
        {
            int characterRelicLevel = 0;
            if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(character, InstanceIDs.IDs.relicLoreID))
                characterRelicLevel = 1;
            if (TinyHelper.SkillRequirements.SafeHasSkillKnowledge(character, InstanceIDs.IDs.mythicLoreID))
                characterRelicLevel = 2;
            return (characterRelicLevel >= relicLevel) ^ Inverted;
        }
    }
}
