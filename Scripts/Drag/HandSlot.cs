using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardData CardData;
    public Image FigureImage;
    public TMP_Text Cost;
    public Hand Hand;
    public DragHandSlot Drag;
    private Tooltip _tooltip;
    public Tooltip Tooltip => _tooltip;
    private void Start()
    {
        Hand = GetComponentInParent<Hand>();
        _tooltip = Main.Instance.Tooltip;
    }
    public void SetAmountMana(int cost)
    {
        Cost.text = cost.ToString();
    }
    public void SetCard(CardData figure)
    {
        CardData.NotNull = true;
        CardData.Icon = figure.Icon;
        CardData.LimitMove = figure.LimitMove;
        CardData.Description = figure.Description;
        CardData.Name = figure.Name;
        CardData.TypeFigure = figure.TypeFigure;
        CardData.Cost = figure.Cost;
        CardData.NameSprite = figure.NameSprite;
        CardData.Priority = figure.Priority;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var slot in Main.Instance.Hand.Slots)
        {
            if (slot.Drag.IsDragging)
                return;
        }
        if (Drag.TryDrag && !Drag.objDelete)
        {
            Drag.transform.SetParent(transform.parent);
            _tooltip.View.SetActive(true);
            _tooltip.SetDescription(CardData.Name ,CardData.Description);
            _tooltip.SetDescription(CardData.Name, CardData.Description);
            _tooltip.RectTransform.transform.position = new Vector3(transform.position.x - (_tooltip.Description.preferredWidth + _tooltip.Name.preferredWidth) / 100, transform.position.y);

        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {

        foreach (var slot in Main.Instance.Hand.Slots)
        {
            if (slot.Drag.IsDragging)
                return;
        }
        if (Drag.TryDrag && !Drag.objDelete)
        {
           
            _tooltip.View.SetActive(false);
            Drag.transform.SetParent(transform);
        }
    }
}
