using System;

[Serializable]
public class FigureData : CardData
{
    public FigureData(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        X = x;
        Y = y; 
        Name = nameSprite;
        ColorFigure = colorFigure;
    }

    public virtual bool CanMove(Slot targetSlot)
    {
        return false;
    }
    public bool IsFigure(Slot slot)
    {
        if (slot.CardData is not FigureData)
            return false;

        return true;
    }
    public override bool TryExpose(Slot slot)
    {
        if (slot.Y < 5)
            return true;

        return false;
    }
    public override bool TryShowBacklight(Slot newSlot)
    {
        if (newSlot.Y > 4 && !newSlot.CardData.NotNull && newSlot.CardData.ColorFigure != ColorFigure.None)
            return true;

        return false;
    }
    public override object Clone()
    {
        return new FigureData(X, Y, Name, ColorFigure)
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
