﻿public class Pawn : FigureData
{

    public Pawn(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        LimitMove = 0;
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 0;
        Priority = 1;
    }

    public override bool CanMove(Slot targetSlot)
    {
        if (IsTravel && TryExpose(targetSlot))
            return true;
        if (targetSlot.CardData.TypeFigure == TypeFigure.Special)
            return false;
        if (targetSlot.CardData is FigureData figure && figure.IsProtected)
            return false;
        if (TypeFigure == TypeFigure.White)
        {
            if (X == targetSlot.X && Y == targetSlot.Y)
                return false;

            if (targetSlot.X == X - 1 && targetSlot.Y == Y - 1 || targetSlot.X == X + 1 && targetSlot.Y == Y - 1)
            {
                if (targetSlot.CardData.NotNull && targetSlot.CardData.TypeFigure != TypeFigure)
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
                if (targetSlot.CardData.NotNull && targetSlot.CardData.TypeFigure != TypeFigure)
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
        string color;
        if (TypeFigure == TypeFigure.White)
            color = "w";
        else
            color = "b";
        return new Pawn(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon,
            IsProtected = IsProtected,
            FreezeName = color + "f_" + Name.Split("_")[1]
        };
    }
}
