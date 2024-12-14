using System;
using UnityEngine;

public class Hog : FigureData
{
    public Hog(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 1;
        LimitMove = 0;
    }
    public override bool CanMove(Slot targetSlot)
    {
        int maxMoveDistance = 1;
        if (IsTravel && TryExpose(targetSlot)) return true;
        if (targetSlot.CardData is SpecialCard) return false;
        if (targetSlot.CardData is FigureData figure && figure.IsProtected) return false;
        if (X == targetSlot.X && Y == targetSlot.Y) return false;

        int deltaX = targetSlot.X - X;
        int deltaY = targetSlot.Y - Y;

        if (Math.Abs(deltaX) > maxMoveDistance || Math.Abs(deltaY) > maxMoveDistance) return false;

        if (X == targetSlot.X || Y == targetSlot.Y)
        {
            int direction = 0;
            int distance = 0; 

            if (X == targetSlot.X)
            {
                direction = targetSlot.Y > Y ? 1 : -1; 
                distance = Mathf.Abs(targetSlot.Y - Y);
            }
            else
            {
                direction = targetSlot.X > X ? 1 : -1;
                distance = Mathf.Abs(targetSlot.X - X);
            }

            for (int i = 1; i < distance; i++)
            {
                int checkX = X + (X == targetSlot.X ? 0 : i * direction);
                int checkY = Y + (Y == targetSlot.Y ? 0 : i * direction);
                if (Board.Instance.Slots[checkY, checkX].CardData.NotNull)
                {
                    return false;
                }
            }

            if (targetSlot.CardData.NotNull && targetSlot.CardData.TypeFigure == TypeFigure)
            {
                return false;
            }
            return true;
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
        return new Hog(X, Y, NameSprite, TypeFigure)
        {
            NotNull = true,
            Icon = Icon,
            IsProtected = IsProtected,
            FreezeName = color + "f_" + NameSprite.Split("_")[1],
            LimitMove = LimitMove
        };
    }
}