using System;

[Serializable]
public class SpecialCard : CardData, ICloneable
{
    public override int LimitMove 
    { 
        get => _limitMove;
        set
        {
            if (value < 0)
            {
                LimitMove = 0;
                ZeroAction();
            }
            else
                _limitMove = value;
        }
    }
    public SpecialCard(int x, int y, string nameSprite, ColorFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
    }
    public virtual void ZeroAction()
    {

    }
    public virtual void Ability()
    {

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
        return false;
    }
}
