using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    public TextMeshProUGUI[] leaderboardTexts; 

    public void Start()
    {
        UpdateLeaderboardUI();
    }

    public void UpdateLeaderboardUI()
    {
        List<ScoreEntry> leaderboard = LeaderboardManager.instance.GetLeaderboard();

        for (int i = 0; i < leaderboardTexts.Length; i++)
        {
            if (i < leaderboard.Count)
            {
                ScoreEntry entry = leaderboard[i];
                leaderboardTexts[i].text = $"Kills: {entry.zombiesKilled}, Damage Taken: {entry.damageTaken}, Damage Dealt: {entry.damageDealt}";
            }
            else
            {
                leaderboardTexts[i].text = "";
            }
        }
    }
}
