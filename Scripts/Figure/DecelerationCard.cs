﻿using System.Collections;
using UnityEngine;

public class DecelerationCard : SpecialCard
{
    public DecelerationCard(int x, int y, string nameSprite, TypeFigure colorFigure) : base(x, y, nameSprite, colorFigure)
    {
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 0;
    }
    public override IEnumerator Recharge(DragHandSlot handSlot, Slot newSlot)
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
            int index = handSlot.OldSlot.transform.GetSiblingIndex();
            handSlot.OldSlot.Hand.RemoveFromHand(index);
            Debug.Log(newSlot.DragSlot.OldSlot.CardData.LimitMove);
            newSlot.DragSlot.OldSlot.CardData.LimitMove++;
            Debug.Log(newSlot.DragSlot.OldSlot.CardData.LimitMove);

            yield return Main.Levels[Main.Instance.IndexLevel].Rival.EndTurn();
            Object.Destroy(handSlot.OldSlot.gameObject);
        }
    }
    public override bool TryExpose(Slot newSlot)
    {
        if (newSlot.CardData.NotNull)
            return true;

        return false;
    }
    public override object Clone()
    {
        return new DecelerationCard(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}
        