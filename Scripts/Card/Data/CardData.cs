using System;

public enum TypeFigure { White, Black, None }

[Serializable]
public class CardData : Data
{
    public CardData(int x, int y, string nameSprite, TypeFigure typeFigure)
    {
        X = x;
        Y = y;
        NameSprite = nameSprite;
        TypeFigure = typeFigure;
    }

    public override object Clone()
    {
        return new CardData(X, Y, NameSprite, TypeFigure)
        {
            NotNull = true,
            Icon = Icon,
            LimitMove = LimitMove
        };
    }

    public override void PlaySound()
    {
        Sounds.PlaySound(Sounds.Get<SoundExposeCard>(), 1, 1);
    }

    public override bool TryExpose(Slot slot)
    {
        return false;
    }
}