using System.Collections;
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

        if (OldSlot.Figure.NotNull && TryDrag)
            transform.position = Vector2.Lerp(transform.position, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.25f);

    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (OldSlot.Figure.NotNull && TryDrag)
        {
            Vector2 pos = transform.position;
            GetComponentInChildren<RectTransform>().localScale = new Vector2(1.25f, 1.25f);
            transform.position = pos;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.75f);
            _image.raycastTarget = false;
            IsDragging = true;
            transform.SetParent(transform.parent.parent);
            Main.Instance.HintPanel.SetActive(true);

        }

    }
    public void OnPointerUp(PointerEventData eventData)
    {

        if (OldSlot.Figure.NotNull && TryDrag)
        {
            GetComponentInChildren<RectTransform>().localScale = new Vector2(1f, 1f);
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.TryGetComponent(out Slot slot))
            {
                if (slot.Figure != null && slot.Figure.NotNull == false)
                    StartCoroutine(RechargeSlot(slot));
                else
                    StartCoroutine(Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position));

            }
            else
                StartCoroutine(Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position));
            _image.raycastTarget = true;
            IsDragging = false;
            Main.Instance.HintPanel.SetActive(false);
            transform.SetParent(OldSlot.transform);
        }

    }
    public IEnumerator RechargeSlot(Slot new_slot)
    {
        if (Characteristics.Instance.Mana < OldSlot.Figure.Cost || new_slot.Y < 5)
        {
            yield return Movement.Smooth(transform, 0.2f, transform.position, OldSlot.transform.position);
        }
        else
        {
            objDelete = true;
            _icon.SetActive(false);
            yield return Movement.TakeOpacity(transform, new_slot.transform.position, _image, 1, 10);
            yield return new WaitForSeconds(0.01f);
            Main.Instance.PlaySound(Main.Instance.AudioExposeFigure, 2, 1);
            Characteristics.Instance.TakeMana(OldSlot.Figure.Cost);
            new_slot.SetFigure(OldSlot.Figure);
            new_slot.DragSlot.TryDrag = false;
            int index = OldSlot.transform.GetSiblingIndex();
            OldSlot.Hand.DisplayedSlot.Add(new_slot);
            OldSlot.Hand.RemoveFromHand(index);
            Destroy(OldSlot.gameObject);
        }
    }

}
