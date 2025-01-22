using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardData CardData;
    public Image FigureImage;
    public Image Image;
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
        CardData = (CardData)figure.Clone();
        FigureImage.sprite = CardData.Icon;
        FigureImage.color = new Color(1, 1, 1, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (var slot in Main.Instance.Hand.Slots)
        {
            if (slot.Drag.IsDragging)
                return;
        }

        Drag.transform.SetParent(transform.parent);
        _tooltip.View.SetActive(true);
        _tooltip.SetDescription(CardData.Name, CardData.Description);
        _tooltip.SetDescription(CardData.Name, CardData.Description);
        _tooltip.RectTransform.transform.position = new Vector3(transform.position.x - (_tooltip.Description.preferredWidth + _tooltip.Name.preferredWidth) / 100, transform.position.y);


    }

    public void OnPointerExit(PointerEventData eventData)
    {

        foreach (var slot in Main.Instance.Hand.Slots)
        {
            if (slot.Drag.IsDragging)
                return;
        }
        _tooltip.View.SetActive(false);
        Drag.transform.SetParent(transform);
    }
}
