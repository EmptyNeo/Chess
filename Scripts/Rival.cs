using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public class Rival
{
    public List<CardData> Deck = new();

    public List<Slot> DisplayedSlot = new();


    public Rival(List<CardData> cardData)
    {
        for (int i = 0; i < cardData?.Count; i++)
        {
            Slot boardSlot = Board.Instance.Slots[cardData[i].Y, cardData[i].X];
            boardSlot.SetCard(cardData[i]);
            DisplayedSlot.Add(boardSlot);
        }
    }
    public IEnumerator IssueCard()
    {
        if (Deck.Count > 0)
        {
            int y = Random.Range(0, 3);
            int x = Random.Range(0, Board.Instance.Slots.GetLength(1));
            if (Board.Instance.Slots[y, x].CardData.NotNull)
                RerollSlot(Board.Instance, ref y, ref x);

            Slot boardSlot = Board.Instance.Slots[y, x];
            int index = Deck.IndexOf(Deck.OrderByDescending(card => card.Priority).Last());
            HandSlot handSlot = Object.Instantiate(PrefabUtil.Load("HandSlot"), Main.Instance.RivalHand).GetComponent<HandSlot>();
            handSlot.SetCard(Deck[index]);
            yield return Movement.Smooth(handSlot.gameObject.transform, 0.25f, handSlot.gameObject.transform.position, boardSlot.transform.position);
            Sounds.PlaySound(Sounds.Get<SoundExposeCard>(), 1, 1);
            boardSlot.SetCard(handSlot.CardData);
            Object.Destroy(handSlot.gameObject);
            DisplayedSlot.Add(boardSlot);
            Deck.RemoveAt(index);
        }
    }
    public bool IsPossibleAttack;
    public IEnumerator Attack()
    {
        List<Slot> attackSlots = new();
        List<Slot> availableDisplayedSlots = new();
        for (int i = 0; i < DisplayedSlot.Count; i++)
        {
            if (IsFigureInSlotCanAttack(DisplayedSlot[i]))
            {
                availableDisplayedSlots.Add(DisplayedSlot[i]);
            }
        }
        if (availableDisplayedSlots.Count > 0)
        {
            int indexAvailableSlots = availableDisplayedSlots.IndexOf(availableDisplayedSlots.OrderByDescending(slot => slot.CardData.Priority).Last());
            attackSlots = GetAttackSlot(attackSlots, availableDisplayedSlots[indexAvailableSlots]);
            if (attackSlots.Count > 0)
            {
                int indexMostPrioritySlot = attackSlots.IndexOf(attackSlots.OrderByDescending(slot => slot.CardData.Priority).First());
                availableDisplayedSlots[indexAvailableSlots].DragSlot.transform.SetParent(availableDisplayedSlots[indexAvailableSlots].transform.parent.parent);
                yield return Movement.Smooth(availableDisplayedSlots[indexAvailableSlots].DragSlot.transform, 0.25f, availableDisplayedSlots[indexAvailableSlots].DragSlot.transform.position, attackSlots[indexMostPrioritySlot].transform.position);

                Sounds.PlaySound(Sounds.Get<SoundExposeFigure>(), 2, 1);
                availableDisplayedSlots[indexAvailableSlots].DragSlot.transform.SetParent(availableDisplayedSlots[indexAvailableSlots].transform);
                attackSlots[indexMostPrioritySlot].SetCard(availableDisplayedSlots[indexAvailableSlots].CardData);
                availableDisplayedSlots[indexAvailableSlots].Nullify();
                availableDisplayedSlots[indexAvailableSlots].DragSlot.transform.position = availableDisplayedSlots[indexAvailableSlots].transform.position;
                Main.Instance.Hand.DisplayedSlot.RemoveAt(Main.Instance.Hand.FindDisplayedSlot(attackSlots[indexMostPrioritySlot]));
                DisplayedSlot[Main.Instance.Hand.FindDisplayedSlot(DisplayedSlot, availableDisplayedSlots[indexAvailableSlots])] = attackSlots[indexMostPrioritySlot];
                IsPossibleAttack = true;
                if (attackSlots[indexMostPrioritySlot].CardData is Pawn && attackSlots[indexMostPrioritySlot].Y > 6)
                {
                    int indexRandomFigure = Random.Range(0, Main.Instance.TransformationFigure.FiguresDataBlack.Count);
                    attackSlots[indexMostPrioritySlot].SetCard(Main.Instance.TransformationFigure.FiguresDataBlack[indexRandomFigure]);
                }

            }
        }
        else
            IsPossibleAttack = false;

        
        yield return null;
    }
    public IEnumerator RandomMove()
    {
        List<Slot> accessSlots = new();
        List<Slot> availableDisplayedSlots = new();
        for (int i = 0; i < DisplayedSlot.Count; i++)
        {
            if (IsFigureInSlotCanMove(DisplayedSlot[i]))
            {
                availableDisplayedSlots.Add(DisplayedSlot[i]);
            }
        }
        if (availableDisplayedSlots.Count > 0)
        {
            int indexAvailableSlots = Random.Range(0, availableDisplayedSlots.Count);
            accessSlots = GetAccessSlot(accessSlots, availableDisplayedSlots[indexAvailableSlots]);
            int indexAccessSlots = Random.Range(0, accessSlots.Count);
            availableDisplayedSlots[indexAvailableSlots].DragSlot.transform.SetParent(availableDisplayedSlots[indexAvailableSlots].transform.parent.parent);
            yield return Movement.Smooth(availableDisplayedSlots[indexAvailableSlots].DragSlot.transform, 0.25f, availableDisplayedSlots[indexAvailableSlots].DragSlot.transform.position, accessSlots[indexAccessSlots].transform.position);
            Sounds.PlaySound(Sounds.Get<SoundExposeFigure>(), 2, 1);
            availableDisplayedSlots[indexAvailableSlots].DragSlot.transform.SetParent(availableDisplayedSlots[indexAvailableSlots].transform);
            accessSlots[indexAccessSlots].SetCard(availableDisplayedSlots[indexAvailableSlots].CardData);
            availableDisplayedSlots[indexAvailableSlots].Nullify();
            availableDisplayedSlots[indexAvailableSlots].DragSlot.transform.position = availableDisplayedSlots[indexAvailableSlots].transform.position;
            DisplayedSlot[Main.Instance.Hand.FindDisplayedSlot(DisplayedSlot, availableDisplayedSlots[indexAvailableSlots])] = accessSlots[indexAccessSlots];
            if (accessSlots[indexAccessSlots].CardData is Pawn && accessSlots[indexAccessSlots].Y > 6)
            {
                int indexRandomFigure = Random.Range(0, Main.Instance.TransformationFigure.FiguresDataBlack.Count);
                accessSlots[indexAccessSlots].SetCard(Main.Instance.TransformationFigure.FiguresDataBlack[indexRandomFigure]);
            }
        }

    }
    public bool IsFigureInSlotCanMove(Slot displayedSlot)
    {
        if (displayedSlot.CardData is FigureData figureData)
        {
            for (int i = 0; i < Board.Instance.Slots.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Instance.Slots.GetLength(1); j++)
                {
                    if (figureData.LimitMove == 0)
                    {
                        if (figureData.CanMove(Board.Instance.Slots[i, j]))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }

    public List<Slot> GetAccessSlot(List<Slot> accessSlot, Slot displayedSlot)
    {
        if (displayedSlot.CardData is FigureData figureData)
        {
            for (int i = 0; i < Board.Instance.Slots.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Instance.Slots.GetLength(1); j++)
                {
                    if (figureData.LimitMove == 0)
                    {
                        if (figureData.CanMove(Board.Instance.Slots[i, j]))
                        {
                            accessSlot.Add(Board.Instance.Slots[i, j]);
                        }
                    }
                }
            }
        }
        return accessSlot;
    }
    public bool IsFigureInSlotCanAttack(Slot displayedSlot)
    {
        if (displayedSlot.CardData is FigureData figureData)
        {
            for (int i = 0; i < Board.Instance.Slots.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Instance.Slots.GetLength(1); j++)
                {
                    if (figureData.LimitMove == 0)
                    {
                        if (figureData.CanMove(Board.Instance.Slots[i, j]) && Board.Instance.Slots[i, j].CardData.NotNull)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    public List<Slot> GetAttackSlot(List<Slot> attackSlot, Slot displayedSlot)
    {
        if (displayedSlot.CardData is FigureData figureData)
        {
            for (int i = 0; i < Board.Instance.Slots.GetLength(0); i++)
            {
                for (int j = 0; j < Board.Instance.Slots.GetLength(1); j++)
                {
                    if (figureData.CanMove(Board.Instance.Slots[i, j]) && Board.Instance.Slots[i, j].CardData.NotNull)
                    {
                        attackSlot.Add(Board.Instance.Slots[i, j]);
                    }
                }
            }
        }
        return attackSlot;
    }
    public void RerollSlot(Board board, ref int y, ref int x)
    {
        y = Random.Range(0, 3);
        x = Random.Range(0, board.Slots.GetLength(1));
        if (board.Slots[y, x].CardData.NotNull)
            RerollSlot(board, ref y, ref x);
    }
}