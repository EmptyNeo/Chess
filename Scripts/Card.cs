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

        Sounds.PlaySound(Sounds.Get<SoundTakeCard>(), 1, 1);
        Vector2 pos = new(0, transform.position.y - Vector2.up.y * 8);
        yield return Movement.Smooth(transform, 0.5f, transform.position, pos);
        yield return new WaitForSeconds(0.2f);
        Main.Instance.DeckData.NameFigures.Add(CardData.Name);
        Main.Instance.AmountChoiceCard++;
        BinarySavingSystem.SaveDeck(Main.Instance.DeckData);
        if (Main.Instance.AmountChoiceCard == 2)
            SceneManager.LoadScene("Map");
    }
}
