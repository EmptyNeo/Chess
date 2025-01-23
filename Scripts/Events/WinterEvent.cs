using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinterEvent : Event
{
    public int Amount;
    public int MaxAmount;
    public TMP_Text Counter;

    private void Start()
    {
        Amount = MaxAmount;
        Counter.text = $"Freezing Left <size=45><color=red>{Amount}</color></size> Turn";

      
    }
    public override IEnumerator StartEvent()
    {
        Amount--;
        Counter.text = $"Freezing Left <size=45><color=red>{Amount}</color></size> Turn";
        if (Amount == 0)
        {
            foreach (Slot slot in Main.Instance.Hand.DisplayedSlot)
            {
                StartCoroutine(slot.DragSlot.ReturnToSlot());
                slot.DragSlot.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
                slot.DragSlot.GetComponentInChildren<Image>().raycastTarget = true;
                slot.DragSlot.GetComponentInChildren<RectTransform>().localScale = new Vector2(1f, 1f);
            }
            Board.Instance.HideHints();
            Board.Instance.HideBacklight();
            Main.Instance.IsCanMove = false;
            Panel.SetActive(true);
            yield return new WaitForSeconds(2f);
            Panel.SetActive(false);
            Amount = MaxAmount;
            Counter.text = $"Freezing Left <size=45><color=red>{Amount}</color></size> Turn";
            Sounds.PlaySound(Sounds.Get<SoundFreezing>(), 1, 1);
         
            for (int i = 0; i < Board.Instance.Slots.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Instance.Slots.GetLength(1); j++)
                {
                    if (Board.Instance.Slots[j, i].CardData.NotNull)
                    {
                        Board.Instance.Slots[j, i].CardData.LimitMove += 2;
                        if (Board.Instance.Slots[j, i].DragSlot.OldSlot.CardData is FigureData figure)
                            Board.Instance.Slots[j, i].DragSlot.OldSlot.FigureImage.sprite = SpriteUtil.Load("pieces", figure.FreezeName);
                    }
                }
                        yield return new WaitForSeconds(0.25f);
            }
            if (Main.Instance.Hand.Slots.Count == 0 && Main.Instance.DeckData.Cards.Count == 0)
                Main.Instance.IsCanMove = true;
        }
    }

}
