using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] BriefingData[] possibleBriefings;

    public BriefingData CurrentBriefing { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public BriefingData StartNewBriefing()
    {
        CurrentBriefing = possibleBriefings[Random.Range(0, possibleBriefings.Length)];
        return CurrentBriefing;
    }

    public (int score, string fanComment) CalculateResult(List<DropZone> zones)
    {
        int targetBits = (int)CurrentBriefing.targetTags;
        int totalTargetCount = CountBits(targetBits);

        float sumPercentage = 0f;
        int zoneCount = 0;

        foreach (var zone in zones)
        {

            if (zone == null) continue;

            OutfitTag equippedTags = zone.GetAllEquippedTags();
            int overlapBits = (int)(CurrentBriefing.targetTags & equippedTags);
            int matchCount = CountBits(overlapBits);

            float percentage = totalTargetCount > 0
                ? (float)matchCount / totalTargetCount * 100f
                : 0f;

            sumPercentage += percentage;
            zoneCount++;
        }

        float baseScore = zoneCount > 0 ? sumPercentage / zoneCount : 0f;

        float roll = Random.Range(0f, 1f);
        float bonus;
        string comment;

        if (baseScore >= 70f)
        {
            if (roll < 0.7f) { bonus = 0.15f; comment = "Styling-nya ikonik banget! 10/10!"; }
            else { bonus = 0.08f; comment = "Cukup oke, sesuai tema."; }
        }
        else if (baseScore >= 40f)
        {
            if (roll < 0.6f) { bonus = 0.08f; comment = "Cukup oke, sesuai tema."; }
            else if (roll < 0.85f) { bonus = 0.15f; comment = "Styling-nya ikonik banget! 10/10!"; }
            else { bonus = 0.05f; comment = "Agak beda dari biasanya, tapi unik."; }
        }
        else

        {
            if (roll < 0.7f) { bonus = 0.05f; comment = "Agak beda dari biasanya, tapi unik."; }
            else { bonus = 0.08f; comment = "Cukup oke, sesuai tema."; }
        }

        int finalScore = Mathf.RoundToInt(baseScore * (1f + bonus));
        return (finalScore, comment);
    }

    int CountBits(int value)
    {
        int count = 0;
        while (value != 0)
        {
            count += value & 1;
            value >>= 1;
        }
        return count;
    }
}