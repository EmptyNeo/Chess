using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Factory
{
    [SerializeField] private List<CardData> _creators = new();
    public List<CardData> Creators => _creators;
   
    public Factory()
    {
        Register(new Pawn(0, 0, "w_pawn", TypeFigure.White));
        Register(new Bishop(0, 0, "w_bishop", TypeFigure.White));
        Register(new Rook(0, 0, "w_rook", TypeFigure.White));
        Register(new Knight(0, 0, "w_knight", TypeFigure.White));
        Register(new Queen(0, 0, "w_queen", TypeFigure.White));
        Register(new Pawn(0, 0, "b_pawn", TypeFigure.Black));
        Register(new Bishop(0, 0, "b_bishop", TypeFigure.Black));
        Register(new Rook(0, 0, "b_rook", TypeFigure.Black));
        Register(new Knight(0, 0, "b_knight", TypeFigure.Black));
        Register(new Queen(0, 0, "b_queen", TypeFigure.Black));
        Register(new Barrel(0, 0, "barrel", TypeFigure.Special));
        Register(new Thunder(0, 0, "thunder", TypeFigure.Special));
        Register(new AccelerationCard(0, 0, "acceleration", TypeFigure.Special));
    }
    public CardData GetFigure(string name)
    {
        foreach (var figure in _creators)
            if (figure.Name == name)
                return figure;

        return null;
    }
    public CardData GetFigure(int index)
    {
        return _creators[index];
    }

    public void Register(CardData creator)
    {
        _creators.Add(creator);
    }

}
