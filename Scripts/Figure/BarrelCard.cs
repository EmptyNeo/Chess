using System.Collections;
using UnityEngine;

public class BarrelCard : SpecialCard
{
    public BarrelCard(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 1;
        LimitMove = 2;
    }
    public override void ZeroAction()
    {
        if (X + 1 < Board.Instance.Slots.GetLength(1) && Board.Instance.Slots[Y, X + 1].CardData.NotNull)
        {
            Board.Instance.Slots[Y, X + 1].Nullify();
        }
        if (X - 1 > 0 && Board.Instance.Slots[Y, X - 1].CardData.NotNull)
        {
            Board.Instance.Slots[Y, X - 1].Nullify();
        }
        Board.Instance.Slots[Y, X].Nullify();
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
            newSlot.SetFigure(this);
            newSlot.DragSlot.TryDrag = false;
            int index = handSlot.OldSlot.transform.GetSiblingIndex();
            handSlot.OldSlot.Hand.DisplayedSlot.Add(newSlot);
            handSlot.OldSlot.Hand.RemoveFromHand(index);

            if (newSlot.X + 1 < Board.Instance.Slots.GetLength(1) && Board.Instance.Slots[newSlot.Y, newSlot.X + 1].CardData.NotNull == false)
            {
                Board.Instance.Slots[newSlot.Y, newSlot.X + 1].SetFigure(new BarrelCard(newSlot.X + 1, newSlot.Y, "barrel", TypeFigure.Special));
            }
            if (newSlot.X - 1 > 0 && Board.Instance.Slots[newSlot.Y, newSlot.X - 1].CardData.NotNull == false)
            {
                Board.Instance.Slots[newSlot.Y, newSlot.X - 1].SetFigure(new BarrelCard(newSlot.X - 1, newSlot.Y, "barrel", TypeFigure.Special));
            }
            
            yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
            Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override bool TryExpose(Slot slot)
    {
        if (slot.Y > 4 && slot.CardData.NotNull == false)
            return true;

        return false;
    }

    public override object Clone()
    {
        return new BarrelCard(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
    /*public override void PlaySound()
    {
        
    }*/
}