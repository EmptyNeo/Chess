using System;
using System.Collections;
using UnityEngine;

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
    public SpecialCard(int x, int y, string nameSprite, TypeFigure TypeFigure) : base(x, y, nameSprite, TypeFigure)
    {
    }
    public virtual void ZeroAction()
    {

    }
    public override void PlaySound()
    {
        Sounds.PlaySound(Sounds.Get("expose_figure"), 2, 1);
    }
    public virtual IEnumerator Recharge(DragHandSlot handSlot, Slot newSlot)
    {
        yield return null;
    }
    public override bool TryExpose(Slot slot)
    {
        return false;
    }

}
