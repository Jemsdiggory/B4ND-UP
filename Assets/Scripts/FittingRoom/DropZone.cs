using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    [SerializeField] string characterId = "AOI";

    [System.Serializable]
    public struct DefaultOutfitItem
    {
        public string category;
        public GameObject defaultObject;
    }

    [SerializeField] List<DefaultOutfitItem> defaultItems;

    public string CharacterId => characterId;

    Dictionary<string, GameObject> currentActiveBySlot = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (var item in defaultItems)
        {
            if (item.defaultObject != null)
            {
                currentActiveBySlot[item.category] = item.defaultObject;
            }
        }
    }

    public void Equip(GameObject onBodyObject, string category)

    {
        if (currentActiveBySlot.TryGetValue(category, out GameObject existing) && existing != null)
            existing.SetActive(false);

        onBodyObject.SetActive(true);
        currentActiveBySlot[category] = onBodyObject;
    }
}