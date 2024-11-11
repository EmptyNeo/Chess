using System;

[Serializable]
public class DataDeck
{
    public string[] figures;
    public DataDeck(Deck deck)
    {
        figures = new string[deck.NameFigures.Count];
        for (int i = 0; i < figures.Length; i++)
        {
            figures[i] = deck.NameFigures[i];
        }
    }
}
