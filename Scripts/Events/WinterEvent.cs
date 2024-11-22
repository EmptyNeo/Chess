using System.Collections;
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

        Panel.transform.SetParent(transform.parent.parent);
    }
    public override IEnumerator StartEvent()
    {
       
        Amount--;
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

            Sounds.PlaySound(Sounds.Get("freezing"), 1, 1);
            GameObject snowWave = Instantiate(PrefabUtil.Load("SnowWave"));
            StartCoroutine(Movement.Smooth(snowWave.transform, 2, snowWave.transform.position, snowWave.transform.position + Vector3.right * 7));
            yield return new WaitForSeconds(2f);
            Destroy(snowWave);
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
        Main.Instance.IsCanMove = true;
    }

}
