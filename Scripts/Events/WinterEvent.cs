using System.Collections;
using TMPro;
using UnityEngine;

public class WinterEvent : Event
{
    public int Amount;
    public int MaxAmount;
    public TMP_Text Counter;

    private void Start()
    {
        Amount = MaxAmount;
        Counter.text = $"Freezing Left <size=45><color=red>{Amount}</color></size> Turn";

        Panel.transform.SetParent(transform.parent.parent);
    }
    public override IEnumerator StartEvent()
    {
        Main.Instance.Board.DisableDragFigure();
        Amount--;
        if (Amount == 0)
        {
            Panel.SetActive(true);
            yield return new WaitForSeconds(2f);
            Panel.SetActive(false);
            Amount = MaxAmount;
            foreach (var slot in Main.Levels[Main.Instance.IndexLevel].Rival.DisplayedSlot)
            {
                slot.CardData.LimitMove += 2;
                if (slot.DragSlot.OldSlot.CardData is FigureData figure)
                    slot.DragSlot.OldSlot.FigureImage.sprite = SpriteUtil.Load("pieces", figure.FreezeName);
            }
            Main.Instance.PlaySound(Main.Instance.AudioFreezing, 1, 1);
            foreach (var slot in Main.Instance.Hand.DisplayedSlot)
            {
                slot.CardData.LimitMove += 2;
                if (slot.DragSlot.OldSlot.CardData is FigureData figure)
                    slot.DragSlot.OldSlot.FigureImage.sprite = SpriteUtil.Load("pieces", figure.FreezeName);
            }
        }
        Counter.text = $"Freezing Left <size=45><color=red>{Amount}</color></size> Turn";
        Main.Instance.Board.EnableDragFigure();
    }

}
