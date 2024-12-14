using System.Collections;
using UnityEngine;

public class BarrelCard : SpecialCard
{

    public BarrelCard(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Name = "Barrel Card";
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 1;
        LimitMove = 2;
        Description = "Puts <size=25>3</size> barrels ";
        Rarity = Rarity.Uncommon;
    }
    public override void ZeroAction()
    {
        Main.Instance.Hand.DisplayedSlot.Remove(Board.Instance.Slots[Y, X]);
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
            newSlot.SetCard(this);

            newSlot.DragSlot.TryDrag = false;
            int index = handSlot.OldSlot.transform.GetSiblingIndex();
            handSlot.OldSlot.Hand.RemoveFromHand(index);
            Main.Instance.Hand.DisplayedSlot.Add(newSlot);
            if (newSlot.X + 1 < Board.Instance.Slots.GetLength(1) && Board.Instance.Slots[newSlot.Y, newSlot.X + 1].CardData.NotNull == false)
            {
                Slot slot = Board.Instance.Slots[newSlot.Y, newSlot.X + 1];
                slot.SetCard(new BarrelCard(newSlot.X + 1, newSlot.Y, "barrel", TypeFigure.None));
                slot.DragSlot.transform.SetParent(slot.DragSlot.transform.parent.parent);
                slot.DragSlot.transform.position = newSlot.transform.position;
                Main.Instance.StartCoroutine(Movement.Smooth(slot.DragSlot.transform,
                                             0.5f,
                                             slot.DragSlot.transform.position,
                                             slot.DragSlot.OldSlot.transform.position));

                slot.DragSlot.transform.SetParent(slot.DragSlot.OldSlot.transform);
                Main.Instance.Hand.DisplayedSlot.Add(slot);
            }
            if (newSlot.X - 1 > -1 && Board.Instance.Slots[newSlot.Y, newSlot.X - 1].CardData.NotNull == false)
            {
                Slot slot = Board.Instance.Slots[newSlot.Y, newSlot.X - 1];
                slot.SetCard(new BarrelCard(newSlot.X - 1, newSlot.Y, "barrel", TypeFigure.None));
                slot.DragSlot.transform.position = newSlot.transform.position;

                slot.DragSlot.transform.SetParent(slot.DragSlot.transform.parent.parent);
                Main.Instance.StartCoroutine(Movement.Smooth(slot.DragSlot.transform,
                                             0.5f,
                                             slot.DragSlot.transform.position,
                                             slot.DragSlot.OldSlot.transform.position));


                slot.DragSlot.transform.SetParent(slot.DragSlot.OldSlot.transform);
                Main.Instance.Hand.DisplayedSlot.Add(slot);
            }
            yield return new WaitForSeconds(0.5f);
            Main.Levels[Main.Instance.IndexLevel].Rival.IssueCard();
            handSlot.transform.SetParent(handSlot.OldSlot.transform);
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
        return new BarrelCard(X, Y, NameSprite, TypeFigure)
        {
            NotNull = true,
            Icon = Icon,
            LimitMove = LimitMove
        };
    }
    public override void PlaySound()
    {
        Sounds.PlaySound(Sounds.Get<SoundExposeBarrel>(), 1, 1);
    }
}