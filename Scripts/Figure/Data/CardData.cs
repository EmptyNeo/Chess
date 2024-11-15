using System;

public enum ColorFigure { White, Black, None }

[Serializable]
public class CardData : Data
{
    public CardData(int x, int y, string nameSprite, ColorFigure colorFigure)
    {
        X = x;
        Y = y;
        Name = nameSprite;
        ColorFigure = colorFigure;
    }

    public override object Clone()
    {
        return new CardData(X, Y, Name, ColorFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }

    public override void PlaySound()
    {
        Main.Instance.PlaySound(Main.Instance.AudioExposeFigure, 2, 1);
    }

    public override bool TryExpose(Slot slot)
    {
        return false;
    }

    public override bool TryShowBacklight(Slot newSlot)
    {
        return true;
    }
}