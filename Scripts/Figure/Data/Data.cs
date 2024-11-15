using System;
using UnityEngine;

public abstract class Data : ICloneable
{
    #region Coordinate
    public int X;
    public int Y;
    #endregion
    public Sprite Icon;
    public int Cost;
    public bool NotNull;
    public string Name;
    public TypeFigure TypeFigure;
    public int Priority;
    protected int _limitMove;
    public virtual int LimitMove
    {
        get { return _limitMove; }
        set
        {
            if (value < 0)
            {
                _limitMove = 0;
            }
            else _limitMove = value;
        }
    }
    public abstract void PlaySound();
    public abstract bool TryExpose(Slot slot);

    public abstract object Clone();
}
