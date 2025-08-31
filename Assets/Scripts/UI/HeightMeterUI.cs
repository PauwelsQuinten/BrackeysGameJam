using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HeightMeterUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _heightText;

    [SerializeField]
    private GameEvent _highScoreChanged;

    private int _height = 0;
    private float _previousHeight = 0;
    private int _previousHighScore = 0;
    public void BiscuitAdded(Component sender, object obj)
    {
        _height++;
        _heightText.text = $"Height: {_height}";

    }

    public void BiscuitDestroyed(Component sender, object obj)
    {
        _height--;
        _heightText.text = $"Height: {_height}";
    }
}
