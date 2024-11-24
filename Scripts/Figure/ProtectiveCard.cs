using System.Collections;
using UnityEngine;

public class ProtectiveCard : SpecialCard
{
    public ProtectiveCard(int x, int y, string nameSprite, TypeFigure TypeFigure) : base(x, y, nameSprite, TypeFigure)
    {
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 1;
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

            if (newSlot.CardData is FigureData figure)
            {
                figure.IsProtected = true;
            }

            yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
            Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override void PlaySound()
    {
        Sounds.PlaySound(Sounds.Get<SoundShield>(), 1, 1);
    }
    public override bool TryExpose(Slot slot)
    {
        if (slot.CardData is FigureData figure)
        {
            if (figure.NotNull)
            {
                return true;
            }
        }
        return false;
    }
    public override object Clone()
    {
        return new ProtectiveCard(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon
        };

    }
}