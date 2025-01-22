using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public int Amount;
    public TMP_Text TAmount;
    public List<CardData> Cards = new();
    public GameObject HandSlot;
    public Transform DeckPoint;
    public Hand Hand;
    public float duration;
    public List<string> NameFigures = new();
    public void AddToDeck(CardData card)
    {
        Cards.Add(card);
        TAmount.text = $"x{++Amount}";
    }
    public void SpawnFigure(CardData cardData)
    {
        HandSlot slot = Instantiate(HandSlot, DeckPoint.position, Quaternion.identity, Hand.transform).GetComponent<HandSlot>();
        slot.Drag.Image.raycastTarget = false;
        StartCoroutine(Hand.AddToHand(slot));
        slot.SetCard(cardData);
        slot.SetAmountMana(cardData.Cost);
        Cards.RemoveAt(0);
        TAmount.text = $"x{--Amount}";
    }
    public void SpawnFigure()
    {
        int index = Random.Range(0, Cards.Count);
        HandSlot slot = Instantiate(HandSlot, DeckPoint.position, Quaternion.identity, Hand.transform).GetComponent<HandSlot>();
        slot.Drag.Image.raycastTarget = false;
        StartCoroutine(Hand.AddToHand(slot));
        slot.SetCard(Cards[index]);
        slot.SetAmountMana(Cards[index].Cost);
        Cards.RemoveAt(index);
        TAmount.text = $"x{--Amount}";

    }
    public void GiveDefaultDeck(Factory factory)
    {
        for (int i = 0; i < 5; i++)
            AddToDeck(factory.GetFigure<Pawn>(TypeFigure.White));
    }
    public void GiveDeckCards(DataDeck dataDeck,Factory factory)
    {
        if (dataDeck != null)
        {
            for (int i = 0; i < dataDeck.figures.Length; i++)
            {
                AddToDeck(factory.GetFigure(dataDeck.figures[i]));
                NameFigures.Add(dataDeck.figures[i]);
            }
        }
    }
    public IEnumerator GiveFigure(AudioClip sound)
    {
        if (Cards.Count == 0)
            yield return null;

        for (int i = 0; i < 3; i++)
        {
            if (Cards.Count > 0)
            {
                Sounds.PlaySound(sound, 1, 1);
                SpawnFigure();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
    public IEnumerator GiveFigure(AudioClip sound, CardData cardData)
    {
        foreach (var card in Hand.Slots)
        {
            card.Drag.TryDrag = false;
        }
        if (Cards.Count == 0)
            yield return null;

        for (int i = 0; i < 3; i++)
        {
            if (Cards.Count > 0)
            {
                Sounds.PlaySound(sound, 1, 1);
                SpawnFigure(cardData);
                yield return new WaitForSeconds(0.2f);
            }
        }
        foreach (var card in Hand.Slots)
        {
            card.Drag.TryDrag = true;
        }

    }
}


