using System.Collections;
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

    public void OnDrag(PointerEventData eventData)
    {

        if (OldSlot.CardData.NotNull && TryDrag)
            transform.position = Vector2.Lerp(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.25f);

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (OldSlot.CardData.NotNull && TryDrag)
        {
            OldSlot.Tooltip.View.SetActive(false);
            Sounds.PlaySound(Sounds.Get<SoundTakeCard>(), 1, 1);
            Vector2 pos = transform.position;
            GetComponentInChildren<RectTransform>().localScale = new Vector2(1.25f, 1.25f);
            transform.position = pos;
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 0.75f);
            Image.raycastTarget = false;
            IsDragging = true;
            transform.SetParent(transform.parent.parent);
            Board.Instance.ShowBacklight(OldSlot.CardData, Characteristics.Instance.Mana >= OldSlot.CardData.Cost);
        }

    }
    public IEnumerator OnPointerUp()
    {
        if (OldSlot.CardData.NotNull && TryDrag)
        {
            GetComponentInChildren<RectTransform>().localScale = new Vector2(1f, 1f);
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 1f);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent(out Slot slot))
            {
                if (OldSlot.CardData is FigureData && slot.CardData.NotNull == false)
                {
                    StartCoroutine(RechargeSlot(slot));
                }
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

            IsDragging = false;
            Main.Instance.HintPanel.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            transform.SetParent(OldSlot.transform);
            Image.raycastTarget = true;
        }

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(OnPointerUp());
    }

    public IEnumerator RechargeSlot(Slot newSlot)
    {
        if (Characteristics.Instance.Mana < OldSlot.CardData.Cost || OldSlot.CardData.TryExpose(newSlot) == false)
        {
            yield return Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position);
        }
        else
        {

            OldSlot.CardData.PlaySound();
            Icon.SetActive(false);
            yield return Movement.TakeOpacity(transform, newSlot.transform.position, Image, 1, 10);
            yield return new WaitForSeconds(0.01f);
            Characteristics.Instance.TakeMana(OldSlot.CardData.Cost);
            newSlot.SetCard(OldSlot.CardData);
            int index = OldSlot.transform.GetSiblingIndex();
            OldSlot.Hand.DisplayedSlot.Add(newSlot);
            OldSlot.Hand.RemoveFromHand(index);
            transform.SetParent(OldSlot.transform);
            Destroy(OldSlot.gameObject);
            Main.Instance.StartCoroutine(Main.Levels[Main.Instance.IndexLevel].Rival.IssueCard());
        }
    }
}
