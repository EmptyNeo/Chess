using UnityEngine;

public class Barrel : SpecialCard
{
    public Barrel(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Name = nameSprite;
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 2;
        LimitMove = 1;
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
    public override void Ability()
    {
        if (X + 1 < Board.Instance.Slots.GetLength(1)  && Board.Instance.Slots[Y, X + 1].CardData.NotNull == false)
        {
            Board.Instance.Slots[Y, X + 1].SetFigure(new Barrel(X + 1, Y, "barrel", ColorFigure.None));
        }
        if (X - 1 > 0 && Board.Instance.Slots[Y, X - 1].CardData.NotNull == false)
        {
            Board.Instance.Slots[Y, X - 1].SetFigure(new Barrel(X - 1, Y, "barrel", ColorFigure.None));
        }
    }
    public override bool TryExpose(Slot slot)
    {
        if (slot.Y < 5 && slot.CardData.NotNull == false)
            return true;

        return false;
    }
    public override bool TryShowBacklight(Slot newSlot)
    {
        if (newSlot.Y > 4 && !newSlot.CardData.NotNull)
            return true;

        return false;
    }
    public override object Clone()
    {
        return new Barrel(X, Y, Name, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
    /*public override void PlaySound()
    {
        
    }*/
}