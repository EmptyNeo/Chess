using System.Collections;
using UnityEngine;

public class FreezingCard : SpecialCard
{
    public FreezingCard(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 2;
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
            int index = handSlot.OldSlot.transform.GetSiblingIndex();
            handSlot.OldSlot.Hand.RemoveFromHand(index);
            newSlot.DragSlot.OldSlot.CardData.LimitMove+=2;
            if (newSlot.DragSlot.OldSlot.CardData is FigureData figure)
                newSlot.DragSlot.OldSlot.FigureImage.sprite = SpriteUtil.Load("pieces", figure.FreezeName);
            yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
            Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override bool TryExpose(Slot slot)
    {
        if (slot.CardData.NotNull)
        {
            return true;
        }
        else
            return false;
    }
    public override object Clone()
    {
        return new FreezingCard(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}