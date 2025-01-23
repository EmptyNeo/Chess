using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform Deck;
    public List<HandSlot> Slots = new();
    public List<HandSlot> AlignetSet = new();
    public List<Slot> DisplayedSlot;
    public float Spacing = 1f;
    public bool IsOnlySpecialCard()
    {
        foreach(var slot in DisplayedSlot)
        {
            if(slot.CardData is not SpecialCard)
                return false;
        }
        return true;
    }
    public bool IsOnlySpecialCardInHand()
    {
        foreach (var slot in Slots)
        {
            if (slot.CardData is SpecialCard specialCard && Board.Instance.TryPossibleExposeSpecialCard(specialCard))
                return false;
        }
        return true;
    }
    public IEnumerator AddToHand(HandSlot slot)
    {
        Slots.Add(slot);
        slot.Image.raycastTarget = false;
        ResetAlignetSet();
        yield return SmoothMovement();
        slot.Image.raycastTarget = true;
    }
    public void RemoveFromHand(int index)
    {
        Slots.RemoveAt(index);
        ResetAlignetSet();
        StartCoroutine(SmoothMovement());
    }
    public Vector2 GetPosition(int i)
    {
        float offset = i * Spacing - ((float)AlignetSet.Count - 1) / 2 * Spacing;
        return transform.position + Vector3.right * offset;
    }
    public IEnumerator SmoothMovement()
    {
        for (int i = 0; i < AlignetSet.Count; i++)
        {
            yield return Movement.Smooth(AlignetSet[i].transform, 0.25f, AlignetSet[i].transform.position, GetPosition(i));
        }
    }

    public void ResetAlignetSet()
    {
        AlignetSet.Clear();
        for (int i = 0; i < Slots.Count; i++)
        {
            if (!Slots[i].Drag.IsDragging)
                AlignetSet.Add(Slots[i]);

        }
    }
    public int FindDisplayedSlot(Slot slot)
    {
        for (int i = 0; i < DisplayedSlot.Count; i++)
        {
            if (DisplayedSlot[i] == slot)
                return i;
        }
        return 0;
    }
    public int FindDisplayedSlot(List<Slot> displayedSlot, Slot slot)
    {
        for (int i = 0; i < displayedSlot.Count; i++)
        {
            if (displayedSlot[i] == slot)
                return i;
        }
        return 0;
    }
}
