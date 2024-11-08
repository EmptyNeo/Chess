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
    public void ShowHints(FigureData figure)
    {
        for (int i = 0; i < Slots.GetLength(0); i++)
        {
            for (int j = 0; j < Slots.GetLength(1); j++)
            {
                if (figure.CanMove(Slots[i, j]))
                {
                    Slots[i, j].Hint.SetActive(true);
                }
            }
        }
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
    public void DisableFirstTurn()
    {
        foreach(Slot slot in Slots)
        {
            if (slot.Figure.IsFirstTurn)
            {
                slot.Figure.IsFirstTurn = false;
            }
        }
    }
}
