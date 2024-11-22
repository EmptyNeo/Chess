using System.Collections;
using UnityEngine;

public class ThunderCard : SpecialCard
{
    public ThunderCard(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 0;
    }
    public override IEnumerator Recharge(DragHandSlot handSlot, Slot newSlot)
    {
        if (Characteristics.Instance.Mana < handSlot.OldSlot.CardData.Cost)
        {
            yield return Movement.Smooth(handSlot.transform, 0.2f, handSlot.transform.position, handSlot.OldSlot.transform.position);
        }
        else
        {
            handSlot.objDelete = true;
            handSlot.Icon.SetActive(false);
            yield return Movement.TakeOpacity(handSlot.transform, newSlot.transform.position, handSlot.Image, 1, 10);
            yield return new WaitForSeconds(0.01f);
            handSlot.OldSlot.CardData.PlaySound();
            Characteristics.Instance.TakeMana(Cost);
            newSlot.SetCard(this);
            newSlot.DragSlot.TryDrag = false;
            int index = handSlot.OldSlot.transform.GetSiblingIndex();
            handSlot.OldSlot.Hand.DisplayedSlot.Add(newSlot);
            handSlot.OldSlot.Hand.RemoveFromHand(index);

            Board.Instance.Slots[newSlot.Y, newSlot.X].Nullify();
            Main.Levels[Main.Instance.IndexLevel].Rival.DisplayedSlot.Remove(Board.Instance.Slots[newSlot.Y, newSlot.X]);

            yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
            Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override void PlaySound()
    {
        Sounds.PlaySound(Sounds.Get("thunder"), 1, 1);
    }
    public override bool TryExpose(Slot slot)
    {

        if (slot.CardData.NotNull && slot.CardData.TypeFigure == TypeFigure.Black)
        {
            return true;
        }
        else
            return false;

    }

    public override object Clone()
    {
        return new ThunderCard(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}
