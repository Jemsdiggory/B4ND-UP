using System.Collections.Generic;
using UnityEngine;

public class FittingRoomPanelManager : MonoBehaviour
{
    public static FittingRoomPanelManager Instance { get; private set; }

    [System.Serializable]
    public struct CategoryPanel
    {
        public string category;
        public GameObject panelRoot;
    }

    [System.Serializable]
    public struct CharacterPanelGroup
    {
        public string characterId;
        public List<CategoryPanel> panels;
    }

    [SerializeField] List<CharacterPanelGroup> characterGroups;

    string currentCharacterId;

    void Awake()
    {
        Instance = this;
        HideAllPanels();
    }

    public void ShowPanelFor(string characterId)

    {
        currentCharacterId = characterId;
        ShowCategory("Cloth");
    }

    public void ShowCategory(string category)
    {
        foreach (var group in characterGroups)
        {
            bool isCurrentCharacter = group.characterId == currentCharacterId;
            foreach (var p in group.panels)
            {
                p.panelRoot.SetActive(isCurrentCharacter && p.category == category);
            }
        }
    }

    public void HideAllPanels()
    {
        foreach (var group in characterGroups)
        {
            foreach (var p in group.panels)
            {
                p.panelRoot.SetActive(false);
            }
        }
    }
}