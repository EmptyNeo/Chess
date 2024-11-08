using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform Deck;
    public List<HandSlot> Slots = new();
    public List<HandSlot> AlignetSet = new();
    public List<Slot> DisplayedSlot;
    public float Spacing = 1f;

    public void AddToHand(HandSlot slot)
    {
        Slots.Add(slot);
        ResetAlignetSet();
        SmoothMovement();
    }
    public void RemoveFromHand(int index)
    {
        Slots.RemoveAt(index);
        ResetAlignetSet();
        SmoothMovement();
    }
    public Vector2 GetPosition(int i)
    {
        float offset = i * Spacing - (AlignetSet.Count / 2 - 0.5f) * Spacing;
        return transform.position + Vector3.right * offset;
    }
    public void SmoothMovement()
    {
        for (int i = 0; i < AlignetSet.Count; i++)
        {
            StartCoroutine(Movement.Smooth(AlignetSet[i].transform, 0.25f, AlignetSet[i].transform.position, GetPosition(i)));
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
        for(int i = 0; i < DisplayedSlot.Count;i++)
        {
            if (DisplayedSlot[i] == slot)
                return i;
        }
        return 0;
    }
}
