using UnityEngine;

public class Knight : FigureData
{
    public Knight(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 1;

        Priority = 3;
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

        if (Mathf.Abs(targetSlot.X - X) == 1 && Mathf.Abs(targetSlot.Y - Y) == 2 || Mathf.Abs(targetSlot.X - X) == 2 && Mathf.Abs(targetSlot.Y - Y) == 1)
        {
            if (targetSlot.CardData.NotNull && targetSlot.CardData.TypeFigure == TypeFigure)
                return false;

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
        return new Knight(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon,
            IsProtected = IsProtected,
            FreezeName = color + "f_" + Name.Split("_")[1],
            LimitMove = LimitMove
        };
    }
}
