using UnityEngine;

public class Bishop : FigureData
{
    public Bishop(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 1;

        Priority = 3;
    }
    public override bool CanMove(Slot targetSlot)
    {
        if (IsTravel&&TryExpose(targetSlot))
            return true;
        if (targetSlot.CardData.TypeFigure == TypeFigure.Special)
            return false;

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
                if (Board.Instance.Slots[Y + i * directionY, X + i * directionX].CardData.NotNull)
                    return false;

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
        return new Bishop(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon,
            IsProtected = IsProtected,
            FreezeName = color + "f_" + Name.Split("_")[1],
            LimitMove = LimitMove
        };
    }
}
