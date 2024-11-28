using System.Collections;
using UnityEngine;

public class DefrostingCard : SpecialCard
{
    public DefrostingCard(int x, int y, string nameSprite, TypeFigure TypeFigure) : base(x, y, nameSprite, TypeFigure)
    {
        Name = "Fireball";
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 2;
        Description = "Defrost shapes\n" +
                     "within a 3 by 3 radius";
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
            Main.Instance.Hand.RemoveFromHand(index);

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
                        Slot slot = Board.Instance.Slots[Y, X];
                        if (slot.CardData.NotNull && slot.CardData is FigureData figure && slot.FigureImage.sprite.name == figure.FreezeName)
                        {
                            figure.LimitMove = 0;
                        }
                    }
                }
            }

            yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
            handSlot.transform.SetParent(handSlot.OldSlot.transform);
            Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override void PlaySound()
    {
        Sounds.PlaySound(Sounds.Get<SoundDefrosting>(), 1, 1);
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
        return new DefrostingCard(X, Y, NameSprite, TypeFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}