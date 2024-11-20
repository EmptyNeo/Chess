using System.Collections.Generic;
using TMPro;
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
    }
    public override void StartEvent()
    {
        Amount--;
        Counter.text = $"Earthquake Left <size=45><color=red>{Amount}</color></size> Turn";
        if (Amount == 0)
        {
            Amount = MaxAmount;
            Counter.text = $"Earthquake Left <size=45><color=red>{Amount}</color></size> Turn";
            RearrangingSlots(Main.Instance.Hand.DisplayedSlot);
        }
    }
    private void RearrangingSlots(List<Slot> displayedSlot)
    {
        List<Slot> rechargeSlots = new();
        List<Slot> deleteSlots = new();
        foreach (var slot in displayedSlot)
        {
            Debug.Log(slot.Y);
            if (slot.Y > 4)
            {
                int randomY = Random.Range(5, 8);
                int randomX = Random.Range(0, Board.Instance.Slots.GetLength(1));
                if (Board.Instance.Slots[randomY, randomX].CardData.NotNull)
                {
                    Reroll(ref randomY, ref randomX);
                }
                Board.Instance.Slots[randomY, randomX].SetCard(slot.CardData);
                rechargeSlots.Add(Board.Instance.Slots[randomY, randomX]);
                deleteSlots.Add(slot);
                slot.Nullify();
            }
        }

        foreach (var slot in rechargeSlots)
            displayedSlot.Add(slot);

        foreach (var slot in deleteSlots)
            displayedSlot.Remove(slot);

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
