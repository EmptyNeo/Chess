using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int X, Y;
    public FigureData Figure;
    public Image FigureImage;
    public Image Outline;
    public GameObject Hint;
    public DragSlot DragSlot;
    public void SetFigure(FigureData figure)
    {
        Figure = (FigureData)figure.Clone();
        Figure.X = X;
        Figure.Y = Y;
        FigureImage.sprite = Figure.Icon;
        FigureImage.color = new Color(1,1,1,1);
    }
    public void Nullify()
    {
        Figure.NotNull = false;
        Figure.X = 0;
        Figure.Y = 0;
        FigureImage.sprite = null;
        FigureImage.color = new Color(1, 1, 1, 0);
    }

}
