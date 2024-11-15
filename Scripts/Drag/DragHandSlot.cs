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
    private Image _image => GetComponent<Image>();
    public GameObject _icon;

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
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.75f);
            _image.raycastTarget = false;
            IsDragging = true;
            transform.SetParent(transform.parent.parent);
            Board.Instance.ShowBacklight(OldSlot.CardData);
            Image image = Main.Instance.HintPanel.transform.GetChild(0).GetComponent<Image>();
            if (Characteristics.Instance.Mana < OldSlot.CardData.Cost)
            {
                image.color = new Color(1, 0, 0, image.color.a);
            }
            else
            {
                image.color = new Color(0, 1, 0, image.color.a);
            }
            Main.Instance.Hand.ResetAlignetSet();
            Main.Instance.Hand.SmoothMovement();
        }

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (OldSlot.CardData.NotNull && TryDrag)
        {
            GetComponentInChildren<RectTransform>().localScale = new Vector2(1f, 1f);
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent(out Slot slot))
            {
               
                if (slot.CardData.NotNull == false)
                    StartCoroutine(RechargeSlot(slot));
                else if (OldSlot.CardData.TryExpose(slot) && slot.CardData.NotNull && OldSlot.CardData is SpecialCard)
                    StartCoroutine(Display(slot));
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
            _image.raycastTarget = true;
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
        if (Characteristics.Instance.Mana < OldSlot.CardData.Cost || OldSlot.CardData.TryExpose(newSlot))
        {
            yield return Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position);
        }
        else
        {
            yield return Display(newSlot);
        }
    }

    private IEnumerator Display(Slot newSlot)
    {
        objDelete = true;
        _icon.SetActive(false);
        yield return Movement.TakeOpacity(transform, newSlot.transform.position, _image, 1, 10);
        yield return new WaitForSeconds(0.01f);
        OldSlot.CardData.PlaySound();
        Characteristics.Instance.TakeMana(OldSlot.CardData.Cost);
        newSlot.SetFigure(OldSlot.CardData);
        newSlot.DragSlot.TryDrag = false;
        int index = OldSlot.transform.GetSiblingIndex();
        OldSlot.Hand.DisplayedSlot.Add(newSlot);
        OldSlot.Hand.RemoveFromHand(index);
        if (newSlot.CardData is SpecialCard specialCard)
            specialCard.Ability();
        yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
        Destroy(OldSlot.gameObject);
    }
}
