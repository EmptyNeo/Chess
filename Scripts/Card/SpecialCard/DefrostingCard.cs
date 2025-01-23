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
                     "and destroy random piece\n" +
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
            handSlot.Icon.SetActive(false);
            yield return Movement.TakeOpacity(handSlot.transform, newSlot.transform.position, handSlot.Image, 1, 10);
            yield return new WaitForSeconds(0.01f);
            handSlot.OldSlot.CardData.PlaySound();
            Characteristics.Instance.TakeMana(Cost);
            int index = handSlot.OldSlot.transform.GetSiblingIndex();
            Main.Instance.Hand.RemoveFromHand(index);
            int iRandom = Random.Range(-1, 2);
            int jRandom = Random.Range(-1, 2);
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int Y = newSlot.Y - i;
                    int X = newSlot.X - j;
                    bool tryOutBordersY = Y > -1 && Y < Board.Instance.Slots.GetLength(0);
                    bool tryOutBordersX = X > -1 && X < Board.Instance.Slots.GetLength(1);
                  
                    if (tryOutBordersY && tryOutBordersX)
                    {
                        Slot slot = Board.Instance.Slots[Y, X];
                        if (iRandom == i && jRandom == j)
                        {
                            slot.Nullify();
                        }
                        else if (slot.CardData.NotNull && slot.CardData is FigureData figure && slot.FigureImage.sprite.name == figure.FreezeName)
                        {
                            figure.LimitMove = 0;
                        }
                    }
                }
            }

            Main.Instance.StartCoroutine(Main.Levels[Main.Instance.IndexLevel].Rival.IssueCard());
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
            if (figure.NotNull)
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