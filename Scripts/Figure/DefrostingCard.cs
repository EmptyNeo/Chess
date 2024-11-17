using System.Collections;
using UnityEngine;

public class DefrostingCard : SpecialCard
{
    public DefrostingCard(int x, int y, string nameSprite, TypeFigure TypeFigure) : base(x, y, nameSprite, TypeFigure)
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

            foreach (var card in Main.Instance.Hand.DisplayedSlot)
            {
                if (card.CardData is FigureData figure && card.FigureImage.sprite.name == figure.FreezeName)
                {
                    figure.LimitMove = -1;
                }
            }


            yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
            Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override bool TryExpose(Slot slot)
    {
        if (slot.CardData is FigureData figure)
        {
            if (figure.TypeFigure == TypeFigure.White && slot.FigureImage.sprite.name == figure.FreezeName)
            {
                return true;
            }
        }
        return false;
    }
    public override object Clone()
    {
        return new DefrostingCard(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}