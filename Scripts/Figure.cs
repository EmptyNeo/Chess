using TMPro;
using UnityEngine;

public class Pawn : FigureData
{

    public Pawn(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        IsFirstTurn = false;
        Name = "Pawn";
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 0;
        Priority = 1;
    }

    public override bool CanMove(Slot targetSlot)
    {
        if(ColorFigure == ColorFigure.White)
        {
            if (X == targetSlot.X && Y == targetSlot.Y)
                return false;

            if (targetSlot.X == X - 1 && targetSlot.Y == Y - 1 || targetSlot.X == X + 1 && targetSlot.Y == Y - 1)
            {
                if (targetSlot.Figure.NotNull && targetSlot.Figure.ColorFigure != ColorFigure)
                    return true;
            }

            if (targetSlot.X == X && targetSlot.Y == Y - 1)
            {
                Debug.Log(targetSlot.Figure.NotNull);
                if (targetSlot.Figure.NotNull == false)
                    return true;
            }

        }
        else
        {
            if (X == targetSlot.X && Y == targetSlot.Y)
                return false;

            if (targetSlot.X == X - 1 && targetSlot.Y == Y + 1 || targetSlot.X == X + 1 && targetSlot.Y == Y + 1)
            {
                if (targetSlot.Figure.NotNull && targetSlot.Figure.ColorFigure != ColorFigure)
                    return true;
            }

            if (targetSlot.X == X && targetSlot.Y == Y + 1)
            {
                Debug.Log(targetSlot.Figure.NotNull);
                if (targetSlot.Figure.NotNull == false)
                    return true;
            }
        }
        return false;
    }
    public override object Clone()
    {
        return new Pawn(X, Y, _nameSprite, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}

public class Bishop : FigureData
{
    public Bishop(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Name = "Bishop";
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 1;

        Priority = 3;
    }
    public override bool CanMove(Slot targetSlot)
    {
        if (targetSlot.X == X && targetSlot.Y == Y)
        {
            Debug.Log("Выбранная ячейка равна ячейке, на которой находится фигура");
            return false;
        }
        if (Mathf.Abs(targetSlot.X - X) == Mathf.Abs(targetSlot.Y - Y))
        {
            int directionX = targetSlot.X > X ? 1 : -1;
            int directionY = targetSlot.Y > Y ? 1 : -1;

            for (int i = 1; i < Mathf.Abs(targetSlot.X - X); i++)
                if (Board.Instance.Slots[Y + i * directionY, X + i * directionX].Figure.NotNull)
                    return false;

            if (targetSlot.Figure.NotNull && targetSlot.Figure.ColorFigure == ColorFigure)
                return false;

            return true;
        }
        return false;
    }
    public override object Clone()
    {
        return new Bishop(X, Y, _nameSprite, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}
public class Rook : FigureData
{
    public Rook(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Name = "Rook";
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 1;

        Priority = 5;
    }
    public override bool CanMove(Slot targetSlot)
    {
        if (X == targetSlot.X && Y == targetSlot.Y)
            return false;

        if (X == targetSlot.X || Y == targetSlot.Y)
        {
            if (X == targetSlot.X)
            {
                int directionY = targetSlot.Y > Y ? 1 : -1;
                for (int i = 1; i < Mathf.Abs(targetSlot.Y - Y); i++)
                {
                    if (Board.Instance.Slots[Y + i * directionY, X].Figure.NotNull)
                    {
                        Debug.Log("На пути ладьи находится фигура");
                        return false;
                    }
                }
            }
            else
            {
                int directionX = targetSlot.X > X ? 1 : -1;
                for (int i = 1; i < Mathf.Abs(targetSlot.X - X); i++)
                {
                    if (Board.Instance.Slots[Y, X + i * directionX].Figure.NotNull)
                    {
                        Debug.Log("На пути ладьи находится фигура");
                        return false;
                    }
                }
            }

            if (targetSlot.Figure.NotNull && targetSlot.Figure.ColorFigure == ColorFigure)
            {
                Debug.Log("На целевой позиции находится фигура того же цвета");
                return false;
            }

            return true;
        }

        return false;
    }
    public override object Clone()
    {
        return new Rook(X, Y, _nameSprite, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };

    }
}
public class Knight : FigureData
{
    public Knight(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Name = "Knight";
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 1;

        Priority = 3;
    }
    public override bool CanMove(Slot targetSlot)
    {
        if (X == targetSlot.X && Y == targetSlot.Y)
            return false;

        if (Mathf.Abs(targetSlot.X - X) == 1 && Mathf.Abs(targetSlot.Y - Y) == 2 || Mathf.Abs(targetSlot.X - X) == 2 && Mathf.Abs(targetSlot.Y - Y) == 1)
        {
            if (targetSlot.Figure.NotNull && targetSlot.Figure.ColorFigure == ColorFigure)
                return false;

            return true;
        }
        return false;
    }
    public override object Clone()
    {
        return new Knight(X, Y, _nameSprite, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}
public class Queen : FigureData
{
    public Queen(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Name = "Queen";
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 2;

        Priority = 9;
    }
    public override bool CanMove(Slot targetSlot)
    {
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
                if (Board.Instance.Slots[Y + i * directionY, X + i * directionX].Figure.NotNull)
                    return false;

            if (targetSlot.Figure.NotNull && targetSlot.Figure.ColorFigure == ColorFigure)
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
                    if (Board.Instance.Slots[Y + i * directionY, X].Figure.NotNull)
                    {
                        Debug.Log("На пути ладьи находится фигура");
                        return false;
                    }
                }
            }
            else
            {
                int directionX = targetSlot.X > X ? 1 : -1;
                for (int i = 1; i < Mathf.Abs(targetSlot.X - X); i++)
                {
                    if (Board.Instance.Slots[Y, X + i * directionX].Figure.NotNull)
                    {
                        Debug.Log("На пути ладьи находится фигура");
                        return false;
                    }
                }
            }

            if (targetSlot.Figure.NotNull &&
                targetSlot.Figure.ColorFigure == ColorFigure)
            {
                Debug.Log("На целевой позиции находится фигура того же цвета");
                return false;
            }

            return true;
        }

        return false;
    }
    public override object Clone()
    {
        return new Queen(X, Y, _nameSprite, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}


