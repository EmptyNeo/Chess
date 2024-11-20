using TMPro;

public class WinterIvent : Ivent
{
    public int Amount;
    public int MaxAmount;
    public TMP_Text Counter;
    private void Start()
    {
        Amount = MaxAmount;
        Counter.text = $"Freezing Left <size=45><color=red>{Amount}</color></size> Turn";
    }
    public override void StartIvent()
    {
        Amount--;
        if (Amount == 0)
        {
            Amount = MaxAmount;
            foreach (var slot in Main.Levels[Main.Instance.IndexLevel].Rival.DisplayedSlot)
            {
                slot.CardData.LimitMove += 2;
                if (slot.DragSlot.OldSlot.CardData is FigureData figure)
                    slot.DragSlot.OldSlot.FigureImage.sprite = SpriteUtil.Load("pieces", figure.FreezeName);
            }
            foreach (var slot in Main.Instance.Hand.DisplayedSlot)
            {
                slot.CardData.LimitMove += 2;
                if (slot.DragSlot.OldSlot.CardData is FigureData figure)
                    slot.DragSlot.OldSlot.FigureImage.sprite = SpriteUtil.Load("pieces", figure.FreezeName);
            }
        }
        Counter.text = $"Freezing Left <size=45><color=red>{Amount}</color></size> Turn";
    }

}
