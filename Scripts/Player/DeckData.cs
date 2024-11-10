using System.Collections;
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

        StartCoroutine(Hand.AddToHand(slot));
        slot.SetFigure(figureData);
        slot.SetAmountMana(figureData.Cost);
        Figures.RemoveAt(0);
        TAmount.text = $"x{--Amount}";
    }
    public void SpawnFigure()
    {
        int index = Random.Range(0, Figures.Count);
        HandSlot slot = Instantiate(HandSlot, Deck.position, Quaternion.identity, Hand.transform).GetComponent<HandSlot>();
        StartCoroutine(Hand.AddToHand(slot));
        slot.SetFigure(Figures[index]);
        slot.SetAmountMana(Figures[index].Cost);
        Figures.RemoveAt(index);
        TAmount.text = $"x{--Amount}";
    }
    public void GiveKitDefault(Factory factory)
    {
        for (int i = 0; i < 8; i++)
            AddToDeck(factory.Figure("w_pawn"));
        for (int i = 0; i < 2; i++)
        {
            AddToDeck(factory.Figure("w_bishop"));
            AddToDeck(factory.Figure("w_knight"));
            AddToDeck(factory.Figure("w_rook"));
        }
        AddToDeck(factory.Figure("w_queen"));
    }

    public IEnumerator GiveFigure(Sounds sounds, AudioClip sound)
    {
        if (Figures.Count == 0)
            yield return null;

        for (int i = 0; i < 3; i++)
        {
            if (Figures.Count > 0)
            {
                sounds.PlaySound(sound, 1, 1);
                SpawnFigure();
                yield return new WaitForSeconds(0.2f);
            }
        }

    }
    public IEnumerator GiveFigure(Sounds sounds, AudioClip sound, FigureData figure)
    {
        if (Figures.Count == 0)
            yield return null;

        for (int i = 0; i < 3; i++)
        {
            if (Figures.Count > 0)
            {
                sounds.PlaySound(sound, 1, 1);
                SpawnFigure(figure);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}


