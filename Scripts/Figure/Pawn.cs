public class Pawn : FigureData
{

    public Pawn(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        LimitMove = 0;
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 0;
        Priority = 1;
    }

    public override bool CanMove(Slot targetSlot)
    {
        if (IsFigure(targetSlot))
            return false;

        if (ColorFigure == ColorFigure.White)
        {
            if (X == targetSlot.X && Y == targetSlot.Y)
                return false;

            if (targetSlot.X == X - 1 && targetSlot.Y == Y - 1 || targetSlot.X == X + 1 && targetSlot.Y == Y - 1)
            {
                if (targetSlot.CardData.NotNull && targetSlot.CardData.ColorFigure != ColorFigure)
                    return true;
            }

            if (targetSlot.X == X && targetSlot.Y == Y - 1)
            {
                if (targetSlot.CardData.NotNull == false)
                    return true;
            }

        }
        else
        {
            if (X == targetSlot.X && Y == targetSlot.Y)
                return false;

            if (targetSlot.X == X - 1 && targetSlot.Y == Y + 1 || targetSlot.X == X + 1 && targetSlot.Y == Y + 1)
            {
                if (targetSlot.CardData.NotNull && targetSlot.CardData.ColorFigure != ColorFigure)
                    return true;
            }

            if (targetSlot.X == X && targetSlot.Y == Y + 1)
            {
                if (targetSlot.CardData.NotNull == false)
                    return true;
            }
        }
        return false;
    }
    public override object Clone()
    {
        return new Pawn(X, Y, Name, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}
