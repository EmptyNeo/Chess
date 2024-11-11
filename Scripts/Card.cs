using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public FigureData Figure;
    public Image Image;
    public TMP_Text Cost;
    public void SetCard(FigureData figure)
    {
        Figure = figure;
        Image.sprite = figure.Icon;
        Cost.text = figure.Cost.ToString();
    }
    public void OnClick()
    {
        Main.Instance.DeckData.NameFigures.Add(Figure.Name);
        BinarySavingSystem.SaveDeck(Main.Instance.DeckData);
        SceneManager.LoadScene("Map");
    }
}
