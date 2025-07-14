using System.Collections;
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
    private Coroutine _tooltipRotine;

    private void Start()
    {
        Hand = GetComponentInParent<Hand>();
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
        _tooltipRotine = StartCoroutine(ActiveTooltip());

    }

    private IEnumerator ActiveTooltip()
    {
        yield return new WaitForSeconds(0.5f);
        if (_tooltipRotine == null)
            yield break;
        Main.Instance.Tooltip.View.gameObject.SetActive(true);
        Main.Instance.Tooltip.SetDescription(CardData.Name, CardData.Description);
        Main.Instance.Tooltip.SetDescription(CardData.Name, CardData.Description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltipRotine = null;

        foreach (var slot in Main.Instance.Hand.Slots)
        {
            if (slot.Drag.IsDragging)
                return;
        }
        Main.Instance.Tooltip.View.gameObject.SetActive(false);
        Drag.transform.SetParent(transform);
     
    }
}
