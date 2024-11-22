using System.Collections;
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
        StartCoroutine(Click());
    }
    public IEnumerator Click()
    {
        Sounds.PlaySound(Sounds.Get("take_card"), 1, 1);
        Vector2 pos = transform.position;
        pos -= Vector2.up * 8;
        pos = new Vector2(0, pos.y);
        yield return Movement.Smooth(transform, 0.5f, transform.position, pos);
        yield return new WaitForSeconds(0.2f);
        Main.Instance.DeckData.NameFigures.Add(CardData.Name);
        BinarySavingSystem.SaveDeck(Main.Instance.DeckData);
        SceneManager.LoadScene("Map");
    }
}
