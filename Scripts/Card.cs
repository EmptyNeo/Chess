using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public CardData CardData;
    public Image Image;
    public TMP_Text Cost;
    public void SetCard(CardData cardData)
    {
        CardData = cardData;
        Image.sprite = cardData.Icon;
        Cost.text = cardData.Cost.ToString();
    }
    public void OnClick()
    {
        Main.Instance.DeckData.NameFigures.Add(CardData.Name);
        BinarySavingSystem.SaveDeck(Main.Instance.DeckData);
        SceneManager.LoadScene("Map");
    }
}
