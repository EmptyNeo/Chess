using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Slot OldSlot;
    public bool TryDrag = true;

    public void OnDrag(PointerEventData eventData)
    {
        if (OldSlot.CardData.LimitMove > 0 || OldSlot.CardData.TypeFigure != TypeFigure.White)
            return;
        if (OldSlot.CardData.NotNull && TryDrag)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OldSlot.CardData.LimitMove > 0 || OldSlot.CardData.TypeFigure != TypeFigure.White)
            return;

        if (TryDrag)
        {
            if (OldSlot.CardData.NotNull)
            {
                if (OldSlot.CardData is FigureData figure && figure.IsTravel)
                    Board.Instance.ShowBacklight(OldSlot.CardData, true);
                else
                    Board.Instance.ShowHints(OldSlot.CardData);

                GetComponentInChildren<RectTransform>().localScale = new Vector2(1.25f, 1.25f);
                GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0.75f);
                GetComponentInChildren<Image>().raycastTarget = false;

                transform.SetParent(transform.parent.parent);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OldSlot.Outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OldSlot.Outline.enabled = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OldSlot.CardData.LimitMove > 0 || OldSlot.CardData.TypeFigure != TypeFigure.White)
            return;

        if (TryDrag && OldSlot.CardData.NotNull)
        {
            GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
            GetComponentInChildren<Image>().raycastTarget = true;
            GetComponentInChildren<RectTransform>().localScale = new Vector2(1f, 1f);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent(out Slot slot))
            {
                if (OldSlot.CardData is FigureData figure && figure.CanMove(slot))
                {
                    RechargeSlot(slot);
                    transform.position = OldSlot.transform.position;
                }
                else
                {
                    StartCoroutine(ReturnToSlot());
                }
            }
            else
            {
                StartCoroutine(ReturnToSlot());
            }

            Board.Instance.HideHints();
            Board.Instance.HideBacklight();
        }
    }
    public IEnumerator ReturnToSlot()
    {
        yield return Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position);
        yield return new WaitForSeconds(0.2f);
        transform.SetParent(OldSlot.transform);
    }
    public void RechargeSlot(Slot newSlot)
    {
        Main.Instance.PlaySound(Main.Instance.AudioExposeFigure, 1, 1);
        CardData cardData = OldSlot.CardData;
        OldSlot.Nullify();
        int index = Main.Instance.Hand.FindDisplayedSlot(OldSlot);
        bool back = true;
        if (newSlot.CardData.NotNull)
        {
            newSlot.Nullify();
            Main.Levels[Main.Instance.IndexLevel].Rival.DisplayedSlot.Remove(newSlot);
            newSlot.SetCard(cardData);
            Main.Instance.Hand.DisplayedSlot[index] = newSlot;
        }
        else
        {

            if (cardData is FigureData figure && figure.IsTravel)
            {
                TryDrag = false;
                figure.IsTravel = false;
                back = false;
            }
            newSlot.SetCard(cardData);
        }
        Main.Instance.Hand.DisplayedSlot[index] = newSlot;
        if (back)
            StartCoroutine(Main.Instance.Back());
    }
}
