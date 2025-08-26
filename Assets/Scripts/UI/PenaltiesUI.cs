using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenaltiesUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> _penaltyImages = new List<Image>();


    public void PenaltiesChanged(Component sender, object obj)
    {
        int penalty = (int)(obj as int?);

        Color newColor = _penaltyImages[penalty - 1].color;
        newColor.a = 0.5f;
        _penaltyImages[penalty - 1].color = newColor;
    }
}
