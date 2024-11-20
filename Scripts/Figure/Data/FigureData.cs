using System;
using UnityEngine;

[Serializable]
public class FigureData : CardData
{
    public override int LimitMove
    {
        get => _limitMove;
        set
        {
            if (value < 0)
            {
                LimitMove = 0;
            }
            else
            {
                _limitMove = value;
                if (_limitMove == 0)
                    Board.Instance.Slots[Y, X].FigureImage.sprite = SpriteUtil.Load("pieces", Name);
            }

           
        }
    }

    public bool IsTravel;

    public bool IsProtected;
    public string FreezeName;
    public FigureData(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        X = x;
        Y = y; 
        Name = nameSprite;
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
        return new FigureData(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon,
            FreezeName = color + "f_" + Name.Split("_")[1],
            LimitMove = LimitMove
        };
    }

    public override void PlaySound()
    {
        Main.Instance.PlaySound(Main.Instance.AudioExposeFigure, 2, 1);
    }
}
