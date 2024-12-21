using System;

[Serializable]
public class FigureData : CardData
{
    
    public bool IsTravel;

    public bool IsProtected;
    public string FreezeName;
    public FigureData(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        X = x;
        Y = y; 
        NameSprite = nameSprite;
        TypeFigure = typeFigure;
        LimitMove = 1;
    }

    public virtual bool CanMove(Slot targetSlot)
    {
        return false;
    }
 
    public override bool TryExpose(Slot slot)
    {
        if (IsTravel)
        {
            if (slot.Y > 4 && !slot.CardData.NotNull)
            {
                return true;
            }
        }
        if (slot.Y > 4 && !slot.CardData.NotNull)
            return true;

        return false;
    }
    public override object Clone()
    {
        string color;
        if (TypeFigure == TypeFigure.White)
            color = "w";
        else
            color = "b";
        return new FigureData(X, Y, NameSprite, TypeFigure)
        {
            NotNull = true,
            Icon = Icon,
            FreezeName = color + "f_" + NameSprite.Split("_")[1],
            LimitMove = LimitMove
        };
    }
}
