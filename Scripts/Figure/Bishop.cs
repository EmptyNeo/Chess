using UnityEngine;

public class Bishop : FigureData
{
    public Bishop(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Icon = SpriteUtil.Load("pieces", nameSprite);
        Cost = 1;

        Priority = 3;
    }
    public override bool CanMove(Slot targetSlot)
    {
        if (IsFigure(targetSlot))
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

            if (targetSlot.CardData.NotNull && targetSlot.CardData.ColorFigure == ColorFigure)
                return false;

            return true;
        }
        return false;
    }

    public override object Clone()
    {
        return new Bishop(X, Y, Name, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}
