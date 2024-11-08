using System;
using UnityEngine;

public enum ColorFigure { White, Black }

[Serializable]
public class FigureData : ICloneable
{
    #region Coordinate
    public int X;
    public int Y;
    #endregion
    public Sprite Icon;
    public int Cost;
    public bool NotNull;
    public string Name;
    public bool IsFirstTurn;
    protected string _nameSprite;
    public ColorFigure ColorFigure;
    public int Priority;
    public FigureData(int x, int y, string nameSprite, ColorFigure colorFigure)
    {
        X = x;
        Y = y;
        NotNull = true;
        IsFirstTurn = true;
        _nameSprite = nameSprite;
        ColorFigure = colorFigure;
    }

    public virtual bool CanMove(Slot targetSlot)
    {
        return true;
    }

    public virtual object Clone()
    {
        return this;
    }
    
}
