using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Slot OldSlot;
    public bool TryDrag = true;

    public void OnDrag(PointerEventData eventData)
    {
        if (OldSlot.Figure.IsFirstTurn || OldSlot.Figure.ColorFigure != ColorFigure.White)
            return;
        if (OldSlot.Figure.NotNull && TryDrag)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OldSlot.Figure.IsFirstTurn || OldSlot.Figure.ColorFigure != ColorFigure.White)
            return;

        if (TryDrag)
        {
            if (OldSlot.Figure.NotNull)
            {
                foreach (Slot s in Board.Instance.Slots)
                {
                    if(s.Figure.NotNull)
                    {
                        Debug.Log(s.Figure.X);
                        Debug.Log(s.Figure.Y);
                    }
                }
                Board.Instance.ShowHints(OldSlot.Figure);
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
        if (OldSlot.Figure.IsFirstTurn || OldSlot.Figure.ColorFigure != ColorFigure.White)
            return;

        if (TryDrag && OldSlot.Figure.NotNull)
        {
            GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
            GetComponentInChildren<Image>().raycastTarget = true;
            GetComponentInChildren<RectTransform>().localScale = new Vector2(1f, 1f);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent(out Slot slot))
            {
                if (OldSlot.Figure.CanMove(slot))
                {
                    RechargeSlot(slot);
                    transform.position = OldSlot.transform.position;
                }
                else
                {
                    StartCoroutine(Movement.Smooth(transform, 0.25f, transform.position, OldSlot.transform.position));
                }
            }
            else
            {
                StartCoroutine(Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position));
            }

            transform.SetParent(OldSlot.transform);
            Board.Instance.HideHints();
        }
    }
    public void RechargeSlot(Slot new_slot)
    {
        Main.Instance.PlaySound(Main.Instance.AudioExposeFigure, 1, 1);
        FigureData figure = OldSlot.Figure;
        OldSlot.Nullify();
        int index = Main.Instance.Hand.FindDisplayedSlot(OldSlot);
        if (new_slot.Figure.NotNull)
        {
            new_slot.Nullify();
            new_slot.SetFigure(figure);

        }
        else
        {
            new_slot.SetFigure(figure);
        }
        Main.Instance.Hand.DisplayedSlot[index] = new_slot;
        StartCoroutine(Main.Instance.Back());
    }
}
