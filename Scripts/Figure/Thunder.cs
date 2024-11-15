using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Thunder : SpecialCard
{
    public Thunder(int x, int y, string nameSprite, TypeFigure typeFigure) : base(x, y, nameSprite, typeFigure)
    {
        Name = nameSprite;
        Icon = SpriteUtil.Load("special_card", nameSprite);
        Cost = 1;
    }
    public override void Ability()
    {
        Board.Instance.Slots[Y, X].Nullify();
        Main.Levels[Main.Instance.IndexLevel].Rival.DisplayedSlot.Remove(Board.Instance.Slots[Y, X]);

    }
    public override bool TryExpose(Slot slot)
    {

        if (slot.CardData.NotNull && slot.CardData.TypeFigure == TypeFigure.Black)
        {
            return true;
        }
        else
            return false;

    }

    public override object Clone()
    {
        return new Thunder(X, Y, Name, TypeFigure)
        {
            NotNull = true,
            Icon = Icon
        };
    }
}
