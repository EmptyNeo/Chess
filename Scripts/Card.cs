using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardData CardData;
    public Image Image;
    public TMP_Text Cost;
    private Tooltip _tooltip;
    private void Start()
    {
        _tooltip = Main.Instance.Tooltip;
    }
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
        print(Main.Instance.AmountChoiceCard);
        if (Main.Instance.AmountChoiceCard < 2)
        {
            Main.Instance.AmountChoiceCard++;
            Sounds.PlaySound(Sounds.Get<SoundTakeCard>(), 1, 1);
            Vector2 pos = new(0, transform.position.y - Vector2.up.y * 8);
            yield return Movement.Smooth(transform, 0.5f, transform.position, pos);
            yield return new WaitForSeconds(0.2f);
            Main.Instance.DeckData.NameFigures.Add(CardData.NameSprite);
          
            BinarySavingSystem.SaveDeck(Main.Instance.DeckData);
            if (Main.Instance.AmountChoiceCard == 2)
            {
                yield return Main.Instance.Transition.AddOpacity(0.75f);
                SceneManager.LoadScene("Map");
            }
        }
    }
    private float _fontSizeDescription;
    private float _fontSizeName;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _tooltip.View.SetActive(true);
        _fontSizeDescription = _tooltip.Description.fontSize;
        _fontSizeName = _tooltip.Name.fontSize;
        _tooltip.Description.fontSize *= 1.3f;
        _tooltip.Name.fontSize *= 1.3f;
        _tooltip.SetDescription(CardData.Name, CardData.Description);
        _tooltip.SetDescription(CardData.Name, CardData.Description);
        _tooltip.RectTransform.transform.position = new Vector3(transform.position.x - (_tooltip.Description.preferredWidth + _tooltip.Name.preferredWidth) / 100, transform.position.y);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.Description.fontSize = _fontSizeDescription;
        _tooltip.Name.fontSize = _fontSizeName;
        _tooltip.View.SetActive(false);
    }
}
