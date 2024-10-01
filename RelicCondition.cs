namespace RelicCondition
{
    using BepInEx;
    using System.Linq;
    using InstanceIDs;
    using CharacterExtensions;

    [BepInPlugin(GUID, NAME, VERSION)]
    public class RelicCondition : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.reliccondition";
        public const string VERSION = "1.1.0";
        public const string NAME = "RelicCondition";

        internal void Awake()
        {
        }

        public static Equipment HasRelicEquippedOrOnBackpack(Character character, int RequiredItemID = 0, int RequiredEnchantID = 0)
        {
            if (character?.Inventory?.Equipment is CharacterEquipment characterEquipment)
            {
                var potentialEquipment = characterEquipment.EquipmentSlots.Where(s => s != null && s.EquippedItem != null).Select(s => s.EquippedItem).ToList();

                foreach (var slot in character.EquippedOnBag().Union(potentialEquipment))
                {
                    var matchingItem = RequiredItemID == 0 || slot.ItemID == RequiredItemID;
                    var matchingEnchant = RequiredEnchantID == 0 || (slot is Equipment equipmentslot && equipmentslot.ActiveEnchantmentIDs.Contains(RequiredEnchantID));

                    if (matchingItem && matchingEnchant)
                    {
                        return slot as Equipment;
                    }
                }
            }
            return null;
        }

        public static bool HasArcaneInfluence(Character character)
        {
            return character?.Inventory?.SkillKnowledge?.IsItemLearned(IDs.arcaneInfluenceID) ?? false;
        }
    }
}
