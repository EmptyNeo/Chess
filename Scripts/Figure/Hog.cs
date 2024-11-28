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
        if (targetSlot.CardData.TypeFigure == TypeFigure.Special)
            return false;

        if (targetSlot.CardData is FigureData figure && figure.IsProtected)
            return false;

        if (X == targetSlot.X && Y == targetSlot.Y)
            return false;

        int dx = Mathf.Abs(targetSlot.X - X);
        int dy = Mathf.Abs(targetSlot.Y - Y);

        if (dx != dy || dx == 0 || dx > 2) return false;

        if (targetSlot.CardData.NotNull && targetSlot.CardData.TypeFigure == TypeFigure)
            return false;

        return true;
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