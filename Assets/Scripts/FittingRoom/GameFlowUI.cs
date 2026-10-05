using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlowUI : MonoBehaviour
{
    public static GameFlowUI Instance { get; private set; }

    [System.Serializable]
    public struct CharacterZoneEntry
    {
        public string characterId;
        public DropZone dropZone;
    }

    [SerializeField] TextMeshProUGUI statusText;
    [SerializeField] Button readyButton;
    [SerializeField] GameObject backToHomeButton;
    [SerializeField] List<CharacterZoneEntry> characterZones;
    [SerializeField] string homeSceneName = "HomeScreen";

    bool hasEquippedSomething;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        DropZone.OnAnyItemEquipped += HandleItemEquipped;
    }

    void OnDisable()
    {
        DropZone.OnAnyItemEquipped -= HandleItemEquipped;
    }

    void Start()
    {
        ShowBriefing();
        readyButton.interactable = false;
        backToHomeButton.SetActive(false);
    }

    void HandleItemEquipped()
    {
        hasEquippedSomething = true;
        readyButton.interactable = true;
    }

    public void SetActiveCharacter(string characterId)
    {
        // dipakai FittingRoomPanelManager buat ganti panel, gak ngaruh ke scoring
    }

    void ShowBriefing()
    {
        BriefingData briefing = ScoreManager.Instance.StartNewBriefing();
        statusText.text = $"Misi: {briefing.missionName}\nTarget: {briefing.targetTags}";
    }

    public void OnReadyPressed()
    {
        if (!hasEquippedSomething) return;

        List<DropZone> allZones = characterZones.Select(e => e.dropZone).ToList();
        var (score, comment) = ScoreManager.Instance.CalculateResult(allZones);
        statusText.text = $"Skor: {score}\nFans: {comment}";

        readyButton.interactable = false;
        backToHomeButton.SetActive(true);
    }

    public void OnBackToHomePressed()
    {
        SceneManager.LoadScene(homeSceneName);
    }
}