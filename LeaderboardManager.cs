using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public int zombiesKilled;
    public int damageTaken;
    public float damageDealt;
}

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager instance;
    private List<ScoreEntry> leaderboard;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadLeaderboard();
    }

    void LoadLeaderboard()
    {
        leaderboard = new List<ScoreEntry>();

        for (int i = 0; i < 5; i++)
        {
            string key = "ScoreEntry" + i;
            if (PlayerPrefs.HasKey(key))
            {
                string json = PlayerPrefs.GetString(key);
                ScoreEntry entry = JsonUtility.FromJson<ScoreEntry>(json);
                leaderboard.Add(entry);
            }
        }
    }

    void SaveLeaderboard()
    {
        for (int i = 0; i < leaderboard.Count; i++)
        {
            string key = "ScoreEntry" + i;
            string json = JsonUtility.ToJson(leaderboard[i]);
            PlayerPrefs.SetString(key, json);
        }
    }

    public void AddNewScore(ScoreEntry newEntry)
    {
        leaderboard.Add(newEntry);
        leaderboard.Sort((a, b) => b.zombiesKilled.CompareTo(a.zombiesKilled)); 
        if (leaderboard.Count > 5)
        {
            leaderboard.RemoveAt(5); 
        }

        SaveLeaderboard();
    }

    public List<ScoreEntry> GetLeaderboard()
    {
        return leaderboard;
    }
}
