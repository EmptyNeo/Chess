using UnityEngine;

public class Rook : FigureData
{
    public Rook(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Name = "Rook";
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 1;
        Priority = 5;
        Description = "It may move horizontally or vertically";
    }
    public override bool CanMove(Slot targetSlot)
    {
        if (IsTravel && TryExpose(targetSlot))
            return true;
        if (targetSlot.CardData.TypeFigure == TypeFigure.Special)
            return false;

        if (targetSlot.CardData is FigureData figure && figure.IsProtected)
            return false;

        if (X == targetSlot.X && Y == targetSlot.Y)
            return false;

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
        return new Rook(X, Y, NameSprite, TypeFigure)
        {
            NotNull = true,
            Icon = Icon,
            IsProtected = IsProtected,
            FreezeName = color + "f_" + NameSprite.Split("_")[1],
            LimitMove = LimitMove
        };

    }
}
