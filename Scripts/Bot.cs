/*using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public Main Main;
    public Factory Factory;
    public DeckData DeckData;
    public Hand Hand;
    public Board Board;

    private List<Slot> _accessMotions;
    private void Start()
    {
        _accessMotions = new List<Slot>();
    }
    public IEnumerator EndTurn()
    {
        if (Hand.Slots.Count > 0)
        {
            int y = Random.Range(0, 3);
            int x = Random.Range(0, Board.Slots.GetLength(1));
            if (Board.Slots[y, x].Figure.NotNull)
                RerollSlot(ref y, ref x);

            Slot slot = Board.Slots[y, x];
            int index = Hand.Slots.IndexOf(Hand.Slots.OrderByDescending(card => card.Figure.Priority).Last());
            yield return Movement.Smooth(Hand.Slots[index].transform, 0.25f, Hand.Slots[index].transform.position, slot.transform.position);
            yield return new WaitForSeconds(0.15f);

            Main.Instance.PlaySound(Main.Instance.AudioExposeFigure, 1, 1);
            slot.SetFigure(Hand.Slots[index].Figure);
            Destroy(Hand.Slots[index].gameObject);
            Hand.Slots.RemoveAt(index);
            Hand.DisplayedSlot.Add(slot);
        }
    }
    public void RerollSlot(ref int y, ref int x)
    {
        y = Random.Range(0, 3);
        x = Random.Range(0, Board.Slots.GetLength(1));
        if (Board.Slots[y, x].Figure.NotNull)
            RerollSlot(ref y, ref x);
    }

    public IEnumerator Move()
    {
        for (int i = 0; i < Hand.DisplayedSlot.Count; i++)
        {
            if (Hand.DisplayedSlot[i].Figure.ColorFigure != ColorFigure.Black)
            {
                Hand.DisplayedSlot.RemoveAt(i);
            }
        }
        if (Hand.DisplayedSlot.Count > 0)
        {
            int indexDisplayedSlot = Random.Range(0, Hand.DisplayedSlot.Count);
            _accessMotions.Clear();
            Debug.Log(Hand.DisplayedSlot[indexDisplayedSlot].Figure.Name);
            AccessMotionFigure(Hand.DisplayedSlot[indexDisplayedSlot].Figure);

            if (_accessMotions.Count > 0)
            {
                int indexAccessMotion = Random.Range(0, _accessMotions.Count);
                Vector2 oldPos = Hand.DisplayedSlot[indexDisplayedSlot].DragSlot.transform.position;

                Hand.DisplayedSlot[indexDisplayedSlot].DragSlot.transform.SetParent(Hand.DisplayedSlot[indexDisplayedSlot].DragSlot.transform.parent.parent);
                yield return Movement.Smooth(Hand.DisplayedSlot[indexDisplayedSlot].DragSlot.transform, 0.25f, Hand.DisplayedSlot[indexDisplayedSlot].DragSlot.transform.position, _accessMotions[indexAccessMotion].DragSlot.transform.position);
                yield return new WaitForSeconds(0.15f);

                Main.Instance.PlaySound(Main.Instance.AudioExposeFigure, 1, 1);
                Hand.DisplayedSlot[indexDisplayedSlot].DragSlot.transform.SetParent(Hand.DisplayedSlot[indexDisplayedSlot].DragSlot.OldSlot.transform);
                if (_accessMotions[indexAccessMotion].Figure.NotNull)
                    Main.Hand.DisplayedSlot.RemoveAt(Main.Hand.FindDisplayedSlot(_accessMotions[indexAccessMotion]));

                _accessMotions[indexAccessMotion].SetFigure(Hand.DisplayedSlot[indexDisplayedSlot].Figure);
                Hand.DisplayedSlot[indexDisplayedSlot].DragSlot.transform.position = oldPos;
                Hand.DisplayedSlot[indexDisplayedSlot].Nullify();
                Hand.DisplayedSlot[indexDisplayedSlot] = _accessMotions[indexAccessMotion];
            }
            else
            {
                yield return Move();
            }
        }
    }
    public void AccessMotionFigure(FigureData figureData)
    {
        for (int i = 0; i < Board.Slots.GetLength(0); i++)
        {
            for (int j = 0; j < Board.Slots.GetLength(1); j++)
            {
                if (figureData.CanMove(Board.Slots[i, j]))
                {
                    Debug.Log(Board.Slots[i, j]);
                    _accessMotions.Add(Board.Slots[i, j]);
                }
            }
        }
    }

}
*/
