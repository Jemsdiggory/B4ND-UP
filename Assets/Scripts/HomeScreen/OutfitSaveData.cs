using System.Collections.Generic;

public static class OutfitSaveData
{
    // characterId -> (category -> nama GameObject item yang dipakai)
    public static Dictionary<string, Dictionary<string, string>> EquippedByCharacter
        = new Dictionary<string, Dictionary<string, string>>();

    public static void SaveEquip(string characterId, string category, string itemName)
    {
        if (!EquippedByCharacter.ContainsKey(characterId))
            EquippedByCharacter[characterId] = new Dictionary<string, string>();

        EquippedByCharacter[characterId][category] = itemName;
    }

    public static string GetEquipped(string characterId, string category)
    {
        if (EquippedByCharacter.TryGetValue(characterId, out var categories) &&
            categories.TryGetValue(category, out string itemName))
        {
            return itemName;
        }
        return null;
    }
}