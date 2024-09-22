using EffectSourceConditions;
using InstanceIDs;
using SideLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyHelper;
using UnityEngine;

namespace RelicKeeper
{
    public class RelicCondition
    {
        public Transform EffectsContainer;
        public Transform ActivationEffectsContainer;

        public RelicCondition(Skill skill)
        {
            EffectsContainer = TinyGameObjectManager.MakeFreshObject(EffectSourceConditions.EffectSourceConditions.EFFECTS_CONTAINER, true, true, skill.transform).transform;
            ActivationEffectsContainer = TinyGameObjectManager.MakeFreshObject(EffectSourceConditions.EffectSourceConditions.EFFECTS_CONTAINER_ACTIVATION, true, true, skill.transform).transform;
        }
        public static RelicCondition Apply(
            Skill skill, int requiredItem, float manaCost=0, float cooldown=2, float staminaCost=0, float healthCost=0, float durabilityCost=0,
            Character.SpellCastType? castType=null, Character.SpellCastModifier? castModifier=null, float? mobileCastMovementMult=null, int? castSheatheRequired=null, bool? castLocomotionEnabled=null,
            int requiredEnchant = 0, int relicLevel = 0
        )
        {
            
            var relicCondition = new RelicCondition(skill);

            // EFFECTS
            var requirementTransform = TinyGameObjectManager.GetOrMake(relicCondition.EffectsContainer.transform, EffectSourceConditions.EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
            var skillReq = requirementTransform.gameObject.AddComponent<SourceConditionEquipment>();
            skillReq.RequiredItemID = requiredItem;
            skillReq.RequiredEnchantID = requiredEnchant;

            var levelReq = requirementTransform.gameObject.AddComponent<SourceConditionRelicLevel>();
            levelReq.relicLevel = relicLevel;

            // ACTIVATION EFFECTS
            requirementTransform = TinyGameObjectManager.GetOrMake(relicCondition.ActivationEffectsContainer.transform, EffectSourceConditions.EffectSourceConditions.SOURCE_CONDITION_CONTAINER, true, true);
            skillReq = requirementTransform.gameObject.AddComponent<SourceConditionEquipment>();
            skillReq.RequiredItemID = requiredItem;
            skillReq.RequiredEnchantID = requiredEnchant;

            levelReq = requirementTransform.gameObject.AddComponent<SourceConditionRelicLevel>();
            levelReq.relicLevel = relicLevel;

            // DYNAMIC SKILL STAT
            var dynamicSkillStat = requirementTransform.gameObject.AddComponent<DynamicSkillStat>();
            dynamicSkillStat.ManaCost = manaCost;
            dynamicSkillStat.HealthCost = healthCost;
            dynamicSkillStat.StaminaCost= staminaCost;
            dynamicSkillStat.Cooldown = cooldown;
            dynamicSkillStat.DurabilityCost= durabilityCost;

            dynamicSkillStat.CastType = castType ?? skill.ActivateEffectAnimType;
            dynamicSkillStat.MobileCastMovementMult = mobileCastMovementMult ?? skill.MobileCastMovementMult;
            dynamicSkillStat.CastModifier = castModifier ?? (mobileCastMovementMult > 0 ? Character.SpellCastModifier.Mobile : skill.CastModifier);
            dynamicSkillStat.CastSheatheRequired = castSheatheRequired ?? skill.CastSheathRequired;
            dynamicSkillStat.CastLocomotionEnabled = castLocomotionEnabled ?? (mobileCastMovementMult > 0 ? true : skill.CastLocomotionEnabled);

            var reducer = relicCondition.ActivationEffectsContainer.gameObject.AddComponent<ReduceDurability>();
            reducer.EquipmentSlot = EquipmentSlot.EquipmentSlotIDs.LeftHand;
            reducer.Durability = durabilityCost;

            return relicCondition;
        }
    }
}
