using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public Slot[,] Slots = new Slot[8, 8];
    public static Board Instance { get; private set; }

    public Sprite White;
    public Sprite Black;
    private void Start()
    {
        Instance = this;
        char[,] Boards =
       {
            { '@','#','@','#','@','#','@','#' },
            { '#','@','#','@','#','@','#','@' },
            { '@','#','@','#','@','#','@','#' },
            { '#','@','#','@','#','@','#','@' },
            { '@','#','@','#','@','#','@','#' },
            { '#','@','#','@','#','@','#','@' },
            { '@','#','@','#','@','#','@','#' },
            { '#','@','#','@','#','@','#','@' },
            { '@','#','@','#','@','#','@','#' },
        };
        int i = 0,
                j = 0,
                limit = 8;
        for (int k = 0; k < transform.childCount; k++)
        {
            if (j == limit)
            {
                i++;
                j = 0;
            }

            if (transform.GetChild(k).gameObject.TryGetComponent(out Slot slot))
            {
                Slots[i, j] = slot;
                Slots[i, j].Y = i;
                Slots[i, j].X = j;
            }
            j++;
        }
        for (i = 0; i < Slots.GetLength(0); i++)
        {
            for (j = 0; j < Slots.GetLength(1); j++)
            {
                var slot = Slots[i, j].GetComponent<Image>();
                if (Boards[i, j] == '@')
                {
                    slot.sprite = White;
                }
                else
                {
                    slot.sprite = Black;
                }

            }
        }

    }
    public void ShowHints(CardData cardData)
    {
        for (int i = 0; i < Slots.GetLength(0); i++)
        {
            for (int j = 0; j < Slots.GetLength(1); j++)
            {
                Slots[i, j].Hint.SetActive(cardData is FigureData figure && figure.CanMove(Slots[i, j]));
            }
        }
    }
    public bool TryPossibleMove(FigureData figureData)
    {
        for (int i = 0; i < Slots.GetLength(0); i++)
        {
            for (int j = 0; j < Slots.GetLength(1); j++)
            {
                if (figureData.CanMove(Slots[i, j]))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void HideHints()
    {
        for (int i = 0; i < Slots.GetLength(0); i++)
        {
            for (int j = 0; j < Slots.GetLength(1); j++)
            {
                Slots[i, j].Hint.SetActive(false);
            }
        }
    }
    public void ShowBacklight(CardData cardData, bool isExpose)
    {
        for (int i = 0; i < Slots.GetLength(0); i++)
        {
            for (int j = 0; j < Slots.GetLength(1); j++)
            {
                if (cardData.TryExpose(Slots[i, j]))
                {
                    Slots[i, j].Backlight.gameObject.SetActive(true);
                    if (isExpose)
                    {
                        Slots[i, j].Backlight.color = new Color(0, 1, 0, Slots[i, j].Backlight.color.a);
                    }
                    else Slots[i, j].Backlight.color = new Color(1, 0, 0, Slots[i, j].Backlight.color.a);
                }
            }
        }
    }
    public void HideBacklight()
    {
        for (int i = 0; i < Slots.GetLength(0); i++)
        {
            for (int j = 0; j < Slots.GetLength(1); j++)
            {
                Slots[i, j].Backlight.gameObject.SetActive(false);
            }
        }
    }
    public void EnableDragFigure()
    {
        foreach (Slot slot in Slots)
        {
            slot.DragSlot.TryDrag = true;
        }
    }
    public void DisableDragFigure()
    {
        foreach (Slot slot in Slots)
        {
            slot.DragSlot.TryDrag = false;

        }
    }
}
