using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;

[Serializable]
public class Rival
{
    public List<FigureData> Figure = new();

    public List<Slot> DisplayedSlot = new();
    private Main main;

    private Board board;
    private List<Slot> _accessMotions = new();
    public Rival(List<FigureData> figure)
    {
        main = Main.Instance;
        board = Board.Instance;
        for (int i = 0; i < figure.Count; i++)
        {
            Slot boardSlot = board.Slots[figure[i].Y, figure[i].X];
            boardSlot.SetCard(figure[i]);
            DisplayedSlot.Add(boardSlot);
        }
    }
    public IEnumerator EndTurn()
    {
        if (Figure.Count > 0)
        {
            int y = Random.Range(0, 3);
            int x = Random.Range(0, board.Slots.GetLength(1));
            if (board.Slots[y, x].CardData.NotNull)
                RerollSlot(board, ref y, ref x);

            Slot boardSlot = board.Slots[y, x];
            int index = Figure.IndexOf(Figure.OrderByDescending(card => card.Priority).Last());
            HandSlot handSlot = Object.Instantiate(PrefabUtil.Load("HandSlot"), main.RivalHand).GetComponent<HandSlot>();
            handSlot.SetCard(Figure[index]);

            yield return Movement.Smooth(handSlot.transform, 0.25f, handSlot.transform.position, boardSlot.transform.position);
            yield return new WaitForSeconds(0.15f);
            Sounds.PlaySound(Sounds.Get<SoundExposeCard>(), 1, 1);
            boardSlot.SetCard(handSlot.CardData);
            Object.Destroy(handSlot.gameObject);
            DisplayedSlot.Add(boardSlot);
            Figure.RemoveAt(index);
        }
        else yield return null;
    }
    private int _counterAccessMotions;
    public IEnumerator Move()
    {
        for (int i = 0; i < DisplayedSlot.Count; i++)
        {
            if (DisplayedSlot[i].CardData.TypeFigure != TypeFigure.Black)
            {
                DisplayedSlot.RemoveAt(i);
            }
        }
        if (DisplayedSlot.Count > 0)
        {
            int indexDisplayedSlot = Random.Range(0, DisplayedSlot.Count);
            _accessMotions.Clear();
            Debug.Log(DisplayedSlot[indexDisplayedSlot].CardData.NameSprite);
            if(DisplayedSlot[indexDisplayedSlot].CardData is FigureData figure)
                AccessMotionFigure(figure);

            if (_accessMotions.Count > 0)
            {
                _counterAccessMotions = 0;
                int indexAccessMotion = Random.Range(0, _accessMotions.Count);
                Vector2 oldPos = DisplayedSlot[indexDisplayedSlot].DragSlot.transform.position;

                DisplayedSlot[indexDisplayedSlot].DragSlot.transform.SetParent(DisplayedSlot[indexDisplayedSlot].DragSlot.transform.parent.parent);
                yield return Movement.Smooth(DisplayedSlot[indexDisplayedSlot].DragSlot.transform, 0.25f, DisplayedSlot[indexDisplayedSlot].DragSlot.transform.position, _accessMotions[indexAccessMotion].DragSlot.transform.position);
                yield return new WaitForSeconds(0.15f);
                Sounds.PlaySound(Sounds.Get<SoundExposeFigure>(), 1, 1);
                DisplayedSlot[indexDisplayedSlot].DragSlot.transform.SetParent(DisplayedSlot[indexDisplayedSlot].DragSlot.OldSlot.transform);
                if (_accessMotions[indexAccessMotion].CardData.NotNull)
                    main.Hand.DisplayedSlot.RemoveAt(main.Hand.FindDisplayedSlot(_accessMotions[indexAccessMotion]));

                _accessMotions[indexAccessMotion].SetCard(DisplayedSlot[indexDisplayedSlot].CardData);
                DisplayedSlot[indexDisplayedSlot].DragSlot.transform.position = oldPos;
                DisplayedSlot[indexDisplayedSlot].Nullify();
                DisplayedSlot[indexDisplayedSlot] = _accessMotions[indexAccessMotion];
            }
            else if(_accessMotions.Count == 0 && _counterAccessMotions <= Figure.Count)
            {
                yield return Move();
            }
        }
    }
    public void AccessMotionFigure(FigureData figureData)
    {
        _counterAccessMotions++;
        for (int i = 0; i < board.Slots.GetLength(0); i++)
        {
            for (int j = 0; j < board.Slots.GetLength(1); j++)
            {
                if (figureData.CanMove(board.Slots[i, j]))
                {
                    _accessMotions.Add(board.Slots[i, j]);
                }
            }
        }
    }
    public void RerollSlot(Board board,ref int y, ref int x)
    {
        y = Random.Range(0, 3);
        x = Random.Range(0, board.Slots.GetLength(1));
        if (board.Slots[y, x].CardData.NotNull)
            RerollSlot(board, ref y, ref x);
    }
}
