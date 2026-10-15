using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterOutfitDisplay : MonoBehaviour
{
    [SerializeField] string characterId = "AOI";

    [System.Serializable]
    public struct OutfitSlot
    {
        public string category;
        public GameObject defaultObject;        // item yang nyala kalau belum ada riwayat
        public List<GameObject> allPossibleItems; // semua item di kategori ini
    }

    [SerializeField] List<OutfitSlot> slots;

    void Start()
    {
        ApplyOutfit();
    }

    void ApplyOutfit()
    {
        foreach (var slot in slots)
        {
            foreach (var item in slot.allPossibleItems)
            {
                if (item != null) item.SetActive(false);
            }

            string savedItemName = OutfitSaveData.GetEquipped(characterId, slot.category);
            GameObject toActivate = null;

            if (!string.IsNullOrEmpty(savedItemName))
                toActivate = slot.allPossibleItems.FirstOrDefault(i => i != null && i.name == savedItemName);

            if (toActivate == null)
                toActivate = slot.defaultObject;

            if (toActivate != null)
                toActivate.SetActive(true);
        }
    }
}