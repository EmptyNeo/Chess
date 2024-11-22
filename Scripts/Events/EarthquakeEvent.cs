using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EarthquakeEvent : Event
{
    public int Amount;
    public int MaxAmount;
    public TMP_Text Counter;
    private void Start()
    {
        Amount = MaxAmount;
        Counter.text = $"Earthquake Left <size=45><color=red>{Amount}</color></size> Turn";

        Panel.transform.SetParent(transform.parent.parent);
    }
    public override IEnumerator StartEvent()
    {
        Amount--;
        Counter.text = $"Earthquake Left <size=45><color=red>{Amount}</color></size> Turn";
        if (Amount == 0)
        {
            if (IsDisplayedSlotNotNull(Main.Instance.Hand.DisplayedSlot))
            {
                Panel.SetActive(true);
                yield return new WaitForSeconds(2f);
                Panel.SetActive(false);
            }
            Amount = MaxAmount;
            Counter.text = $"Earthquake Left <size=45><color=red>{Amount}</color></size> Turn";

            Main.Instance.IsCanMove = false;
            yield return RearrangingSlots(Main.Instance.Hand.DisplayedSlot);
            Main.Instance.IsCanMove = true;
        }
    }
    public bool IsDisplayedSlotNotNull(List<Slot> displayedSlot)
    {
        foreach (var slot in displayedSlot)
        {
            if (slot.Y > 4 && slot.CardData.NotNull)
            {
                return true;
            }
        }
        return false;
    }
    private IEnumerator RearrangingSlots(List<Slot> displayedSlot)
    {
        List<Slot> rechargeSlots = new();
        List<Slot> deleteSlots = new();

        int amountSlotMoveThanFour = 0;
        foreach (var slot in displayedSlot)
        {
            if (slot.Y > 4)
            {
                amountSlotMoveThanFour++;
                StartCoroutine(Movement.AddSmooth(slot.DragSlot.transform, 1, 1.3f, 1));
            }
        }
        if (amountSlotMoveThanFour > 0)
        {
            StartCoroutine(CameraShake.Instance.ShakeCamera(0.05f));
            Sounds.PlaySound(Sounds.Get("earthquake"), 1, 1);
            yield return new WaitForSeconds(0.15f);
        }
        foreach (var slot in displayedSlot)
        {
            if (slot.Y > 4)
            {
                int randomY = Random.Range(5, 8);
                int randomX = Random.Range(0, Board.Instance.Slots.GetLength(1));
                if (Board.Instance.Slots[randomY, randomX].CardData.NotNull)
                {
                    Reroll(ref randomY, ref randomX);
                }
                slot.DragSlot.transform.SetParent(Board.Instance.Slots[randomY, randomX].transform.parent.parent);
                StartCoroutine(Movement.Smooth(slot.DragSlot.transform, 0.25f, slot.DragSlot.transform.position, Board.Instance.Slots[randomY, randomX].transform.position));
                yield return new WaitForSeconds(0.15f);
                StartCoroutine(Movement.TakeSmooth(slot.DragSlot.transform, 1.3f, 1, 1));
                yield return new WaitForSeconds(0.15f);
                Sounds.PlaySound(Sounds.Get("expose_figure"), 1, 1);
                slot.DragSlot.transform.SetParent(slot.DragSlot.OldSlot.transform);
                Board.Instance.Slots[randomY, randomX].SetCard(slot.CardData);
                slot.DragSlot.transform.position = slot.DragSlot.OldSlot.transform.position;
                slot.DragSlot.transform.position = slot.DragSlot.OldSlot.transform.position;
                rechargeSlots.Add(Board.Instance.Slots[randomY, randomX]);
                deleteSlots.Add(slot);
                slot.Nullify();
            }
        }
        CameraShake.Instance.StopShake();
        foreach (var slot in deleteSlots)
            displayedSlot.Remove(slot);

        foreach (var slot in rechargeSlots)
            displayedSlot.Add(slot);


    }
    private void Reroll(ref int randomY, ref int randomX)
    {
        randomY = Random.Range(5, 8);
        randomX = Random.Range(0, Board.Instance.Slots.GetLength(1));
        if (Board.Instance.Slots[randomY, randomX].CardData.NotNull)
        {
            Reroll(ref randomY, ref randomX);
        }
    }

}
