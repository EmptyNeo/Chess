using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckData : MonoBehaviour
{
    public int Amount;
    public TMP_Text TAmount;
    public List<FigureData> Figures = new();
    public GameObject HandSlot;
    public Transform Deck;
    public Hand Hand;
    public float duration;

    public void AddToDeck(FigureData figure)
    {
        Figures.Add(figure);
        TAmount.text = $"x{++Amount}";
    }
    public void SpawnFigure(FigureData figureData)
    {
        HandSlot slot = Instantiate(HandSlot, Deck.position, Quaternion.identity, Hand.transform).GetComponent<HandSlot>();

        Hand.AddToHand(slot);
        slot.SetFigure(figureData);
        slot.SetAmountMana(figureData.Cost);
        Debug.Log(Figures.IndexOf(figureData));
        Figures.RemoveAt(0);
        TAmount.text = $"x{--Amount}";
    }
    public void SpawnFigure()
    {
        int index = Random.Range(0, Figures.Count);
        HandSlot slot = Instantiate(HandSlot, Deck.position, Quaternion.identity, Hand.transform).GetComponent<HandSlot>();

        Hand.AddToHand(slot);
        slot.SetFigure(Figures[index]);
        slot.SetAmountMana(Figures[index].Cost);
        Figures.RemoveAt(index);
        TAmount.text = $"x{--Amount}";
    }
    
}


