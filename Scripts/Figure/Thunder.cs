using UnityEngine;

public class Thunder : SpecialCard
{
    public Thunder(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Name = nameSprite;
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 1;   
    }   
    public override void Ability()
    {
        Board.Instance.Slots[Y, X].Nullify();
    }
    public override bool TryExpose(Slot slot)
    {
        if (slot.CardData.ColorFigure == ColorFigure.Black)
        {
            return true;
        }
        else
            return false;
    }
    public override bool TryShowBacklight(Slot newSlot)
    {
        if (newSlot.CardData.NotNull && newSlot.CardData.ColorFigure == ColorFigure.Black)
            return true;

        return false;
    }
    public override object Clone()
    {
        return new Thunder(X, Y, Name, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}