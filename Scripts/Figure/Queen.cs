﻿using UnityEngine;

public class Queen : FigureData
{
    public Queen(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 2;

        Priority = 9;
    }
    public override bool CanMove(Slot targetSlot)
    {
        if (IsFigure(targetSlot))
            return false;


        if (targetSlot.X == X && targetSlot.Y == Y)
            return false;

        if (CanMoveBishop(targetSlot) || CanMoveRook(targetSlot))
            return true;

        return false;
    }
    public bool CanMoveBishop(Slot targetSlot)
    {
        if (Mathf.Abs(targetSlot.X - X) == Mathf.Abs(targetSlot.Y - Y))
        {
            int directionX = targetSlot.X > X ? 1 : -1;
            int directionY = targetSlot.Y > Y ? 1 : -1;

            for (int i = 1; i < Mathf.Abs(targetSlot.X - X); i++)
                if (Board.Instance.Slots[Y + i * directionY, X + i * directionX].CardData.NotNull)
                    return false;

            if (targetSlot.CardData.NotNull && targetSlot.CardData.ColorFigure == ColorFigure)
                return false;

            return true;
        }
        return false;
    }
    public bool CanMoveRook(Slot targetSlot)
    {
        if (X == targetSlot.X || Y == targetSlot.Y)
        {
            if (X == targetSlot.X)
            {
                int directionY = targetSlot.Y > Y ? 1 : -1;
                for (int i = 1; i < Mathf.Abs(targetSlot.Y - Y); i++)
                {
                    if (Board.Instance.Slots[Y + i * directionY, X].CardData.NotNull)
                    {
                        return false;
                    }
                }
            }
            else
            {
                int directionX = targetSlot.X > X ? 1 : -1;
                for (int i = 1; i < Mathf.Abs(targetSlot.X - X); i++)
                {
                    if (Board.Instance.Slots[Y, X + i * directionX].CardData.NotNull)
                    {
                        return false;
                    }
                }
            }

            if (targetSlot.CardData.NotNull &&
                targetSlot.CardData.ColorFigure == ColorFigure)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public override object Clone()
    {
        return new Queen(X, Y, Name, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}