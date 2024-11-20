using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EarthquakeIvent : Ivent
{
    public int Amount;
    public int MaxAmount;
    public TMP_Text Counter;
    private void Start()
    {
        Amount = MaxAmount;
        Counter.text = $"Freezing Left <size=45><color=red>{Amount}</color></size> Turn";
    }
    public override void StartIvent()
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
            int randomY = Random.Range(1, Board.Instance.Slots.GetLength(0) - 1);
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

        foreach (var slot in rechargeSlots)
            displayedSlot.Add(slot);

        foreach (var slot in deleteSlots)
            displayedSlot.Remove(slot);

    }
    private void Reroll(ref int randomY, ref int randomX)
    {
        randomY = Random.Range(1, Board.Instance.Slots.GetLength(0) - 1);
        randomX = Random.Range(0, Board.Instance.Slots.GetLength(1));
        if (Board.Instance.Slots[randomY, randomX].CardData.NotNull)
        {
            Reroll(ref randomY, ref randomX);
        }
    }

}
