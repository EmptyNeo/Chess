using System.Collections;
using UnityEngine;

public class AccelerationCard : SpecialCard
{
    public AccelerationCard(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Name = nameSprite;
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 1;
    }
    public override IEnumerator Recharge(DragHandSlot handSlot, Slot newSlot)
    {
        if(Characteristics.Instance.Mana < handSlot.OldSlot.CardData.Cost)
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
            newSlot.DragSlot.OldSlot.CardData.LimitMove--;
            yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
            Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override bool TryExpose(Slot newSlot)
    {
        if (newSlot.CardData.TypeFigure == TypeFigure.White && newSlot.CardData.NotNull)
            return true;
        return false;
    }
    public override void PlaySound()
    {
        base.PlaySound();
    }
    public override object Clone()
    {
        return new AccelerationCard(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon,
        };
    }
}