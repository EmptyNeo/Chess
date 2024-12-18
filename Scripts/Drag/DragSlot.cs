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
        if (Main.Instance.IsCanMove && OldSlot.CardData.NotNull && TryDrag)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OldSlot.CardData.LimitMove > 0 || OldSlot.CardData.TypeFigure != TypeFigure.White)
            return;

        if (Main.Instance.IsCanMove && TryDrag)
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

        if (Main.Instance.IsCanMove && TryDrag && OldSlot.CardData.NotNull)
        {
            GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
            GetComponentInChildren<Image>().raycastTarget = true;
            GetComponentInChildren<RectTransform>().localScale = new Vector2(1f, 1f);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent(out Slot slot))
            {
                if (OldSlot.CardData is FigureData figure && figure.CanMove(slot))
                {
                    if (Main.Instance.DeckData.Cards.Count > 0 || Main.Instance.Hand.Slots.Count > 0)
                    {
                        Main.Instance.IsCanMove = false;
                    }

                    StartCoroutine(RechargeSlot(slot));
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

            transform.SetParent(OldSlot.transform);
            Board.Instance.HideHints();
            Board.Instance.HideBacklight();
        }
    }
    public IEnumerator ReturnToSlot()
    {
        yield return Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position);
        yield return new WaitForSeconds(0.2f);
    }
    public IEnumerator RechargeSlot(Slot newSlot)
    {
        Slot oldSlot = OldSlot;
        Sounds.PlaySound(Sounds.Get<SoundExposeFigure>(), 2, 1);

        CardData cardData = OldSlot.CardData;
        OldSlot.Nullify();
        int index = Main.Instance.Hand.FindDisplayedSlot(OldSlot);
        bool back = true;
        if (newSlot.CardData.NotNull)
        {

            StartCoroutine(ShakeUtil.Instance.Shake(0.1f, 0.05f));
            newSlot.Nullify();
            Main.Levels[Main.Instance.IndexLevel].Rival.DisplayedSlot.RemoveAt(Main.Instance.Hand.FindDisplayedSlot(Main.Levels[Main.Instance.IndexLevel].Rival.DisplayedSlot, newSlot));
            newSlot.SetCard(cardData);
            Main.Instance.Hand.DisplayedSlot[index] = newSlot;
        }
        else
        {
            if (cardData is FigureData figure && figure.IsTravel)
            {

                newSlot.DragSlot.TryDrag = false;
                Board.Instance.EnableDragFigure();
                Main.Instance.IsCanMove = false;
                figure.IsTravel = false;
                back = false;
            }
            newSlot.SetCard(cardData);
        }
        Main.Instance.Hand.DisplayedSlot[index] = newSlot;
        if (newSlot.Y < 1 && newSlot.CardData is Pawn)
        {
           Main.Instance.TransformationFigure.Init(oldSlot, newSlot);
        }
        else if (back)
        {
            yield return new WaitForSeconds(0.3f);
            yield return Main.Instance.Back();
        }
    }
}
