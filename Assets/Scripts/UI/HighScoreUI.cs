using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HighScoreUI : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private HighScoreManager _highScoreManager;

    private void Start()
    {
        _highScoreManager = GameObject.FindAnyObjectByType<HighScoreManager>();
        if (_highScoreManager == null )
        {
            Debug.Log("No HighScoreManager FOUND!!!!");
            return;
        }

        UpdateHighScoreDisplay();
    }

    private void UpdateHighScoreDisplay()
    {
        List<HighScoreEntry> highScores = _highScoreManager.GetHighScores();
        string displayText = "";

        for (int i = 0; i < highScores.Count; i++) 
        {
            displayText += $"{i + 1}. {highScores[i].playerName}: {highScores[i].height} meter\n";
        }

        _scoreText.text = displayText;
    }
}
