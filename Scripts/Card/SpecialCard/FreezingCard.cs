using System.Collections;
using UnityEngine;

public class FreezingCard : SpecialCard
{
    public FreezingCard(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Name = "Snowball";
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 2;
        Description =  "Freezes shapes\n" +
                      "within a 3 by 3 radius";
        Rarity = Rarity.Rare;
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

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int Y = newSlot.Y - i;
                    int X = newSlot.X - j;
                    bool tryOutBordersY = Y > -1 || Y < Board.Instance.Slots.GetLength(0);
                    bool tryOutBordersX = X > -1 || X < Board.Instance.Slots.GetLength(1);
                    if (tryOutBordersY && tryOutBordersX)
                    {
                        CardData card = Board.Instance.Slots[Y, X].CardData;
                        if (card.NotNull && card is FigureData figure)
                        {
                            figure.LimitMove += 2;
                            Board.Instance.Slots[Y, X].FigureImage.sprite = SpriteUtil.Load("pieces", figure.FreezeName);
                        }
                    }
                }
            }

            Main.Levels[Main.Instance.IndexLevel].Rival.IssueCard();
            handSlot.transform.SetParent(handSlot.OldSlot.transform);
            Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override void PlaySound()
    {
        Sounds.PlaySound(Sounds.Get<SoundFreezing>(), 1, 1);
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
        return new FreezingCard(X, Y, NameSprite, TypeFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}