using System.Collections;
using UnityEngine;

public class TreasonEvent : Event
{
    public override IEnumerator StartEvent()
    {
        Slot changeSlot = null;
        for (int i = 0; i < Board.Instance.Slots.GetLength(0); i++)
        {
            for (int j = 0; j < Board.Instance.Slots.GetLength(1); j++)
            {
                if (i < 3)
                {
                    if (Board.Instance.Slots[i, j].CardData.NotNull == false)
                    {
                        changeSlot = Board.Instance.Slots[i, j];
                        break;
                    }
                }
            }
        }
        foreach (var slot in Main.Instance.Hand.DisplayedSlot)
        {
            if (slot.CardData is Queen && slot.CardData.TypeFigure == TypeFigure.White)
            {

                Main.Instance.IsCanMove = false;
                Panel.SetActive(true);
                yield return new WaitForSeconds(2f);
                Panel.SetActive(false);
                slot.CardData.Icon = SpriteUtil.Load("pieces", "b_queen");
                slot.DragSlot.TryDrag = false;
                yield return Movement.Smooth(slot.DragSlot.transform, 1f, slot.DragSlot.transform.position, changeSlot.DragSlot.transform.position);
                yield return new WaitForSeconds(0.5f);
                changeSlot.SetCard(slot.CardData);
                changeSlot.CardData.TypeFigure = TypeFigure.Black;
                Sounds.PlaySound(Sounds.Get<SoundExposeFigure>(), 1, 1);
                slot.DragSlot.transform.position = slot.DragSlot.OldSlot.transform.position;
                slot.Nullify();
                Main.Levels[Main.Instance.IndexLevel].Rival.DisplayedSlot.Add(changeSlot);
                Main.Instance.Hand.DisplayedSlot.Remove(slot);

                Main.Instance.IsCanMove = true;
                break;
            }
        }
    }
}