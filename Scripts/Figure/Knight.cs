using UnityEngine;

public class Knight : FigureData
{
    public Knight(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 1;

        Priority = 3;
    }
    public override bool CanMove(Slot targetSlot)
    {
        if (IsFigure(targetSlot))
            return false;


        if (X == targetSlot.X && Y == targetSlot.Y)
            return false;

        if (Mathf.Abs(targetSlot.X - X) == 1 && Mathf.Abs(targetSlot.Y - Y) == 2 || Mathf.Abs(targetSlot.X - X) == 2 && Mathf.Abs(targetSlot.Y - Y) == 1)
        {
            if (targetSlot.CardData.NotNull && targetSlot.CardData.ColorFigure == ColorFigure)
                return false;

            return true;
        }
        return false;
    }

    public override object Clone()
    {
        return new Knight(X, Y, Name, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}
