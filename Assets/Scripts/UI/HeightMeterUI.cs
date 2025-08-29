using UnityEngine;
using UnityEngine.UI;

public class HeightMeterUI : MonoBehaviour
{
    [SerializeField]
    private Image _heightMeter;
    [SerializeField] 
    private float _maxHeight = 50;

    [SerializeField]
    private GameEvent _highScoreChanged;

    private float _height = 0;
    public void HeightChanged(Component sender, object obj)
    {
        _height = (float)obj;
        _heightMeter.fillAmount = _height / _maxHeight;
    }

    public void BiscuitDestroyed(Component sender, object obj)
    {
        //_height--;
        //_heightText.text = $"Height: {_height}";
    }
}
