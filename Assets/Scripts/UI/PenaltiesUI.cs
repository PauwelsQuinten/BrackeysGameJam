using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenaltiesUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> _penaltyImages = new List<Image>();

    [SerializeField]
    private Sprite _brokenCookie;
    [SerializeField]
    private Sprite _fullCookie;


    public void PenaltiesChanged(Component sender, object obj)
    {
        int penalty = (int)(obj as int?);
        if (penalty > 3) return;
        if (penalty == 0)
        {
            foreach(Image image in _penaltyImages)
            {
                image.sprite = _fullCookie;
            }
            return;
        }

        _penaltyImages[penalty - 1].sprite = _brokenCookie;
    }
}
