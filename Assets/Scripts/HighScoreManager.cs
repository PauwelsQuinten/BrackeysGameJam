using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private List<HighScoreEntry> highScores;
    private const string HighScoreKey = "HighScoreTable";
    private const int MaxHighScores = 10; // Max number of scores to keep

    private void Awake()
    {
        highScores = new List<HighScoreEntry>();
        LoadHighScores();
    }

    public void AddScore(string playerName, int height)
    {
        highScores.Add(new HighScoreEntry { playerName = playerName, height = height });

        highScores = highScores.OrderByDescending(entry => entry.height).ToList();

        if (highScores.Count > MaxHighScores)
        {
            highScores = highScores.Take(MaxHighScores).ToList();
        }

        SaveHighScores();
    }

    private void LoadHighScores()
    {
        highScores.Clear();
        string json = PlayerPrefs.GetString(HighScoreKey, "");
        if (!string.IsNullOrEmpty(json))
        {
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            highScores = data.heights;
        }
    }

    private void SaveHighScores()
    {
        HighScoreData data = new HighScoreData { heights = highScores };
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(HighScoreKey, json);
        PlayerPrefs.Save();
    }

    public List<HighScoreEntry> GetHighScores()
    {
        return highScores;
    }

    [System.Serializable]
    private class HighScoreData
    {
        public List<HighScoreEntry> heights;
    }
}
