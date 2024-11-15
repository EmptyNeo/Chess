using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandSlot : MonoBehaviour
{
    public int X, Y;
    public CardData CardData;
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
    public void SetFigure(CardData figure)
    {
        CardData = (CardData)figure.Clone();
        CardData.X = X;
        CardData.Y = Y;
        FigureImage.sprite = CardData.Icon;
        FigureImage.color = new Color(1, 1, 1, 1);
    }
    public void Nullify()
    {
        CardData.NotNull = false;
        CardData = null;
        FigureImage.sprite = null;
        FigureImage.color = new Color(1, 1, 1, 0);
    }

/*    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var slot in Main.Instance.Hand.Slots)
        {
            if (slot.Drag.IsDragging)
                return;
        }
        if(!Drag.IsDragging && !Drag.objDelete)
            StartCoroutine(Movement.Smooth(Drag.transform, 0.1f, Drag.transform.position, Drag.transform.position + new Vector3(0, 0.2f, 0)));
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        foreach (var slot in Main.Instance.Hand.Slots)
        {
            if (slot.Drag.IsDragging)
                return;
        }
        if (!Drag.IsDragging && !Drag.objDelete)
            StartCoroutine(Movement.Smooth(Drag.transform, 0.1f, Drag.transform.position, Drag.OldSlot.transform.position));
    }*/
}
