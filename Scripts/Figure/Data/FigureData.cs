using System;

[Serializable]
public class FigureData : CardData
{
    public FigureData(int x, int y, string nameSprite, TypeFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        X = x;
        Y = y; 
        Name = nameSprite;
        TypeFigure = colorFigure;
    }

    public virtual bool CanMove(Slot targetSlot)
    {
        return false;
    }
 
    public override bool TryExpose(Slot slot)
    {
        if (slot.Y > 4 && !slot.CardData.NotNull)
            return true;

        return false;
    }
    public override object Clone()
    {
        return new FigureData(X, Y, Name, TypeFigure)
        {
            NotNull= true,
            Icon = Icon
        };
    }

    public override void PlaySound()
    {
        Main.Instance.PlaySound(Main.Instance.AudioExposeFigure, 2, 1);
    }
}
