using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HeightMeterUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _heightText;

    private int _height = 0;
    private float _previousHeight = 0;
    public void HeightChanged(Component sender, object obj)
    {
        float height = (float)(obj as float?);
        if(_previousHeight - height > 0.3f) _height--;
        else if (height - _previousHeight > 0.3f) _height++;
        _previousHeight = height;

        _heightText.text = $"Height: {_height}";
    }

    public void BiscuitDestroyed(Component sender, object obj)
    {
        //_height--;
        //_heightText.text = $"Height: {_height}";
    }
}
