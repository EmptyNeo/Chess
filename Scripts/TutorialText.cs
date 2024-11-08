using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public TMP_Text Tooltip;
    public GameObject Panel;
    public void SetText(string text)
    {
        Tooltip.text = text;
    }
    public void EnablePanel()
    {
        Panel.SetActive(true);  
    }
    public void DisablePanel()
    {
        Panel.SetActive(false);
    }
}

public class Tutorial
{
    public IEnumerator Enable(TutorialText tutorialText)
    {
        tutorialText.SetText("IS YOUR HAND");

        yield return null;
    }
    public void Disable(TutorialText tutorialText)
    {

    }
}