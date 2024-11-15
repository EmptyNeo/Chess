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
        Register(new Pawn(0, 0, "w_pawn", ColorFigure.White));
        Register(new Bishop(0, 0, "w_bishop", ColorFigure.White));
        Register(new Rook(0, 0, "w_rook", ColorFigure.White));
        Register(new Knight(0, 0, "w_knight", ColorFigure.White));
        Register(new Queen(0, 0, "w_queen", ColorFigure.White));
        Register(new Pawn(0, 0, "b_pawn", ColorFigure.Black));
        Register(new Bishop(0, 0, "b_bishop", ColorFigure.Black));
        Register(new Rook(0, 0, "b_rook", ColorFigure.Black));
        Register(new Knight(0, 0, "b_knight", ColorFigure.Black));
        Register(new Queen(0, 0, "b_queen", ColorFigure.Black));
        Register(new Barrel(0, 0, "barrel", ColorFigure.None));
        Register(new Thunder(0, 0, "thunder", ColorFigure.None));
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
