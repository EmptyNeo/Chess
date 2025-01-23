using System.Collections;
using UnityEngine;

public class DecelerationCard : SpecialCard
{
    public DecelerationCard(int x, int y, string nameSprite, TypeFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Name = "Snail Card";
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 0;
        Description = "Slows down the selected piece";
        Rarity = Rarity.Common;
    }
    public override IEnumerator Recharge(DragHandSlot handSlot, Slot newSlot)
    {
        if (Characteristics.Instance.Mana < handSlot.OldSlot.CardData.Cost)
        {
            yield return Movement.Smooth(handSlot.transform, 0.2f, handSlot.transform.position, handSlot.OldSlot.transform.position);
        }
        else
        {
            handSlot.Icon.SetActive(false);
            yield return Movement.TakeOpacity(handSlot.transform, newSlot.transform.position, handSlot.Image, 1, 10);
            yield return new WaitForSeconds(0.01f);
            handSlot.OldSlot.CardData.PlaySound();
            Characteristics.Instance.TakeMana(Cost);
            int index = handSlot.OldSlot.transform.GetSiblingIndex();
            handSlot.OldSlot.Hand.RemoveFromHand(index);
            newSlot.DragSlot.OldSlot.CardData.LimitMove++;
            Main.Instance.StartCoroutine(Main.Levels[Main.Instance.IndexLevel].Rival.IssueCard());
            handSlot.transform.SetParent(handSlot.OldSlot.transform);
            Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override void PlaySound()
    {

    }
    public override bool TryExpose(Slot newSlot)
    {
        if (newSlot.CardData.TypeFigure != TypeFigure.White && newSlot.CardData.NotNull)
            return true;

        return false;
    }
    public override object Clone()
    {
        return new DecelerationCard(X, Y, NameSprite, TypeFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}
        