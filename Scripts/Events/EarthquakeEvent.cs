using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        Panel.transform.SetParent(transform.parent.parent);
    }
    public override IEnumerator StartEvent()
    {
        Amount--;
        Counter.text = $"Earthquake Left <size=45><color=red>{Amount}</color></size> Turn";
        if (Amount == 0)
        {
            if (IsDisplayedSlotNotNull(Main.Instance.Hand.DisplayedSlot) || IsDisplayedSlotNotNull(Main.Levels[Main.Instance.IndexLevel].Rival.DisplayedSlot))
            {
                Panel.SetActive(true);
                yield return new WaitForSeconds(2f);
                Panel.SetActive(false);
            }
            Amount = MaxAmount;
            Counter.text = $"Earthquake Left <size=45><color=red>{Amount}</color></size> Turn";
            Main.Instance.IsCanMove = false;
            yield return RearrangingSlots(Main.Instance.Hand.DisplayedSlot, Main.Levels[Main.Instance.IndexLevel].Rival.DisplayedSlot);

            Sounds.AudioSource.Stop();
            if(Main.Instance.Hand.Slots.Count == 0 && Main.Instance.DeckData.Cards.Count == 0)
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
    private IEnumerator RearrangingSlots(List<Slot> displayedSlot, List<Slot> rivalDisplayedSlot)
    {
        List<Slot> rechargeSlots = new();
        List<Slot> deleteSlots = new();

        List<Slot> rivalRechargeSlots = new();
        List<Slot> rivalDeleteSlots = new();

        bool IsSlotMoveThanFour = displayedSlot?.Any(slot => slot.Y > 4) ?? false;
        bool IsSlotMoveLessThanThree = displayedSlot?.Any(slot => slot.Y < 3) ?? false;

        foreach (var slot in displayedSlot)
        {
            if (slot.Y > 4)
            {
                StartCoroutine(Movement.AddSmooth(slot.DragSlot.transform, 1, 1.3f, 1));
            }
        }
        foreach (var slot in rivalDisplayedSlot)
        {
            if (slot.Y < 3)
            {
                StartCoroutine(Movement.AddSmooth(slot.DragSlot.transform, 1, 1.3f, 1));
            }
        }
        if (IsSlotMoveThanFour || IsSlotMoveLessThanThree)
        {
            Main.Instance.Canvas.renderMode = RenderMode.WorldSpace;
            StartCoroutine(ShakeUtil.Instance.Shake(0.05f));
            Sounds.PlaySound(Sounds.Get<SoundEarthquake>(), 1, 1);
        }
        foreach (var slot in displayedSlot)
        {
            if (slot.Y > 4)
            {
                int randomY = Random.Range(5, 8);
                int randomX = Random.Range(0, Board.Instance.Slots.GetLength(1));
                if (Board.Instance.Slots[randomY, randomX].CardData.NotNull)
                {
                    Reroll(ref randomY, ref randomX, 5,8);
                }
                Slot rechargeSlot = Board.Instance.Slots[randomY, randomX];
                slot.DragSlot.transform.SetParent(rechargeSlot.transform.parent.parent);
                yield return Movement.Smooth(slot.DragSlot.transform, 0.15f, slot.DragSlot.transform.position, rechargeSlot.transform.position);
                yield return Movement.TakeSmooth(slot.DragSlot.transform, 1.3f, 1, 1);
                Sounds.PlaySound(Sounds.Get<SoundExposeFigure>(), 1, 1);
                slot.DragSlot.transform.SetParent(slot.DragSlot.OldSlot.transform);
                rechargeSlot.SetCard(slot.CardData);
                slot.DragSlot.transform.position = slot.DragSlot.OldSlot.transform.position;
                rechargeSlots.Add(rechargeSlot);
                deleteSlots.Add(slot);
                slot.Nullify();
            }
        }
        foreach (var slot in rivalDisplayedSlot)
        {
            if (slot.Y < 3)
            {
                int randomY = Random.Range(0, 3);
                int randomX = Random.Range(0, Board.Instance.Slots.GetLength(1));
                if (Board.Instance.Slots[randomY, randomX].CardData.NotNull)
                {
                    Reroll(ref randomY, ref randomX, 0,3);
                }
                Slot rechargeSlot = Board.Instance.Slots[randomY, randomX];
                slot.DragSlot.transform.SetParent(rechargeSlot.transform.parent.parent);
                yield return Movement.Smooth(slot.DragSlot.transform, 0.15f, slot.DragSlot.transform.position, rechargeSlot.transform.position);
                yield return Movement.TakeSmooth(slot.DragSlot.transform, 1.3f, 1, 1);
                Sounds.PlaySound(Sounds.Get<SoundExposeFigure>(), 1, 1);
                slot.DragSlot.transform.SetParent(slot.DragSlot.OldSlot.transform);
                rechargeSlot.SetCard(slot.CardData);
                slot.DragSlot.transform.position = slot.DragSlot.OldSlot.transform.position;
                rivalRechargeSlots.Add(rechargeSlot);
                rivalDeleteSlots.Add(slot);
                slot.Nullify();
            }
        }
        ShakeUtil.Instance.StopShake();

        Main.Instance.Canvas.renderMode = RenderMode.ScreenSpaceCamera;

        StartCoroutine(Sounds.GraduallyReducingVolume());
        Sounds.AudioSource.Stop();
        foreach (var slot in deleteSlots)
            displayedSlot.Remove(slot);

        foreach (var slot in rechargeSlots)
            displayedSlot.Add(slot);

        foreach (var slot in rivalDeleteSlots)
            rivalDisplayedSlot.Remove(slot);

        foreach (var slot in rivalRechargeSlots)
            rivalDisplayedSlot.Add(slot);


    }
    private int AmountSlotMoveThan(List<Slot> displayedSlot,bool isThan)
    {
        int amountSlotMoveThan = 0;
        foreach (var slot in displayedSlot)
        {
            if (isThan)
            {
                amountSlotMoveThan++;
            }
        }
        return amountSlotMoveThan;
    }
    private void Reroll(ref int randomY, ref int randomX, int start, int end)
    {
        randomY = Random.Range(start, end);
        randomX = Random.Range(0, Board.Instance.Slots.GetLength(1));
        if (Board.Instance.Slots[randomY, randomX].CardData.NotNull)
        {
            Reroll(ref randomY, ref randomX, start, end);
        }
    }

}
