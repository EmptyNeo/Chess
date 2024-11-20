using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int X, Y;
    public CardData CardData;
    public Image FigureImage;
    public Image Outline;
    public GameObject Hint;
    public Image Backlight;
    public DragSlot DragSlot;

    public void SetCard(CardData cardData)
    {
        CardData = (CardData)cardData.Clone();
        CardData.X = X;
        CardData.Y = Y;
        FigureImage.sprite = CardData.Icon;
        FigureImage.color = new Color(1, 1, 1, 1);
    }
    public void Nullify()
    {
        CardData = new FigureData(0, 0, "", TypeFigure.None)
        {
            NotNull = false,
            Icon = null
        };
        FigureImage.color = new Color(1, 1, 1, 0);
    }

}
