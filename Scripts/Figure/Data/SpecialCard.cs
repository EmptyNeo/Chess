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
    public SpecialCard(int x, int y, string nameSprite, TypeFigure colorFigure) : base(x, y, nameSprite, colorFigure)
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
    public virtual IEnumerator Recharge(DragHandSlot handSlot, Slot newSlot)
    {
        if (Characteristics.Instance.Mana < handSlot.OldSlot.CardData.Cost)
        {
            yield return Movement.Smooth(handSlot.transform, 0.2f, handSlot.transform.position, handSlot.OldSlot.transform.position);
        }
        else
        {
            handSlot.objDelete = true;
            handSlot.Icon.SetActive(false);
            yield return Movement.TakeOpacity(handSlot.transform, newSlot.transform.position, handSlot.Image, 1, 10);
            yield return new WaitForSeconds(0.01f);
            handSlot.OldSlot.CardData.PlaySound();
            Characteristics.Instance.TakeMana(Cost);
            newSlot.SetFigure(this);
            newSlot.DragSlot.TryDrag = false;
            int index = handSlot.OldSlot.transform.GetSiblingIndex();
            handSlot.OldSlot.Hand.DisplayedSlot.Add(newSlot);
            handSlot.OldSlot.Hand.RemoveFromHand(index);
            if (newSlot.CardData is SpecialCard card)
                card.Ability();
            yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
            UnityEngine.Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override bool TryExpose(Slot slot)
    {
        return false;
    }

}
