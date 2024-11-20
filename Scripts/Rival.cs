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
    public Rival(List<FigureData> figure)
    {
        main = Main.Instance;
        board = Board.Instance;
        for (int i = 0; i < figure.Count; i++)
        {
            StartFigure(figure[i].X, figure[i].Y, figure[i]);
        }
    }
    public void StartFigure(int x, int y, FigureData figure)
    {
        Slot boardSlot = board.Slots[y, x];
        boardSlot.SetCard(figure);
        DisplayedSlot.Add(boardSlot);
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

            main.PlaySound(main.AudioExposeFigure, 1, 1);
            boardSlot.SetCard(handSlot.CardData);
            Object.Destroy(handSlot.gameObject);
            DisplayedSlot.Add(boardSlot);
            Figure.RemoveAt(index);
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
