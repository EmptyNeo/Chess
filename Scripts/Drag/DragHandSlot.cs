using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragHandSlot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public HandSlot OldSlot;
    public bool IsDragging;

    public bool TryDrag = true;
    public Image Image => GetComponent<Image>();
    public GameObject Icon;

    public bool objDelete;
    public void OnDrag(PointerEventData eventData)
    {

        if (OldSlot.CardData.NotNull && TryDrag)
            transform.position = Vector2.Lerp(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.25f);

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (OldSlot.CardData.NotNull && TryDrag)
        {
            Vector2 pos = transform.position;
            GetComponentInChildren<RectTransform>().localScale = new Vector2(1.25f, 1.25f);
            transform.position = pos;
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 0.75f);
            Image.raycastTarget = false;
            IsDragging = true;
            transform.SetParent(transform.parent.parent);
            Board.Instance.ShowBacklight(OldSlot.CardData, Characteristics.Instance.Mana >= OldSlot.CardData.Cost);
            Main.Instance.Hand.ResetAlignetSet();
            Main.Instance.Hand.SmoothMovement();
        }

    }
    int j;
    public void OnPointerUp(PointerEventData eventData)
    {
        if (OldSlot.CardData.NotNull && TryDrag)
        {
            GetComponentInChildren<RectTransform>().localScale = new Vector2(1f, 1f);
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 1f);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent(out Slot slot))
            {
                if (OldSlot.CardData is FigureData && slot.CardData.NotNull == false)
                    StartCoroutine(RechargeSlot(slot));
                else if (OldSlot.CardData is SpecialCard specialCard && OldSlot.CardData.TryExpose(slot))
                {
                    StartCoroutine(specialCard.Recharge(this, slot));
                }
                else
                {
                    StartCoroutine(Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position));
                }

            }
            else
            {
                StartCoroutine(Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position));

            }


            Board.Instance.HideBacklight();
            Image.raycastTarget = true;
            IsDragging = false;
            Main.Instance.HintPanel.SetActive(false);
            transform.SetParent(OldSlot.transform);
            if (!objDelete)
            {
                Main.Instance.Hand.ResetAlignetSet();
                Main.Instance.Hand.SmoothMovement();
            }
        }

    }
    public IEnumerator RechargeSlot(Slot newSlot)
    {
        if (Characteristics.Instance.Mana < OldSlot.CardData.Cost || OldSlot.CardData.TryExpose(newSlot) == false)
        {
            yield return Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position);
        }
        else
        {
            objDelete = true;
            Icon.SetActive(false);
            yield return Movement.TakeOpacity(transform, newSlot.transform.position, Image, 1, 10);
            yield return new WaitForSeconds(0.01f);
            OldSlot.CardData.PlaySound();
            Characteristics.Instance.TakeMana(OldSlot.CardData.Cost);
            newSlot.SetFigure(OldSlot.CardData);
            newSlot.DragSlot.TryDrag = false;
            int index = OldSlot.transform.GetSiblingIndex();
            OldSlot.Hand.DisplayedSlot.Add(newSlot);
            OldSlot.Hand.RemoveFromHand(index);
            yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
            Destroy(OldSlot.gameObject);
        }
    }
}
