using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    public TMP_Text Tooltip;
    public GameObject Panel;
    public GameObject[] pos;
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
public static class Tutorial
{
    public static IEnumerator Enable(TutorialText tutorialText)
    {
        tutorialText.SetText("IS YOUR HAND");

        yield return Movement.Smooth(tutorialText.Panel.transform, 0.25f, tutorialText.Panel.transform.position, tutorialText.pos[0].transform.position);
        yield return Movement.Smooth(tutorialText.Tooltip.transform, 0.25f, tutorialText.Tooltip.transform.position, tutorialText.pos[0].transform.position + new Vector3(0, 1f, 0));
        yield return new WaitForSeconds(1f);

        yield return Movement.Smooth(tutorialText.Panel.transform, 0.25f, tutorialText.Panel.transform.position, tutorialText.pos[1].transform.position);
        yield return Movement.Smooth(tutorialText.Tooltip.transform, 0.25f, tutorialText.Tooltip.transform.position, tutorialText.pos[1].transform.position + new Vector3(0, 1f, 0));
        tutorialText.SetText("IS YOUR DECK");
        yield return new WaitForSeconds(1f);
        yield return Movement.Smooth(tutorialText.Panel.transform, 0.25f, tutorialText.Panel.transform.position, tutorialText.pos[2].transform.position);
        yield return Movement.Smooth(tutorialText.Tooltip.transform, 0.25f, tutorialText.Tooltip.transform.position, tutorialText.pos[2].transform.position + new Vector3(0, 1f, 0));
        tutorialText.SetText("IS YOUR MANA");

        yield return new WaitForSeconds(1f);
        tutorialText.DisablePanel();
    }
    public static void Disable(TutorialText tutorialText)
    {

    }
}
