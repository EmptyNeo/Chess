using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int X, Y;
    public FigureData Figure;
    public Image FigureImage;
    public TMP_Text Cost;
    public Hand Hand;
    public DragHandSlot Drag;

    private void Start()
    {
        Hand = GetComponentInParent<Hand>();   
    }
    public void SetAmountMana(int cost)
    {
        Cost.text = cost.ToString();
    }
    public void SetFigure(FigureData figure)
    {
        Figure = (FigureData)figure.Clone();
        Figure.X = X;
        Figure.Y = Y;
        FigureImage.sprite = Figure.Icon;
        FigureImage.color = new Color(1, 1, 1, 1);
    }
    public void Nullify()
    {
        Figure.NotNull = false;
        Figure = null;
        FigureImage.sprite = null;
        FigureImage.color = new Color(1, 1, 1, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!Drag.IsDragging && !Drag.objDelete)
            StartCoroutine(Movement.Smooth(Drag.transform, 0.1f, Drag.transform.position, Drag.transform.position + new Vector3(0, 0.2f, 0)));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Drag.IsDragging && !Drag.objDelete)
            StartCoroutine(Movement.Smooth(Drag.transform, 0.1f, Drag.transform.position, Drag.OldSlot.transform.position));
    }
}
