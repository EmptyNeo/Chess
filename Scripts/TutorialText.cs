using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    public bool skip;
    public TMP_Text Tooltip;
    public RaycastInverter Panel;
    public GameObject Object;
    public GameObject[] pos;
    public IEnumerator SetText(string text)
    {
        StringBuilder stringBuilder = new();
        for (int i = 0; i < text.Length; i++)
        {
            stringBuilder.Append(text[i]);
            Tooltip.text = stringBuilder.ToString();
            Sounds.PlaySound(Sounds.Get<SoundTyping>(), 0.8f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            skip = true;
        }
    }
    public void EnablePanel()
    {
        Object.SetActive(true);
    }
    public void DisablePanel()
    {
        Object.SetActive(false);
    }
    
}
public static class Tutorial
{
   
    public static IEnumerator Enable(TutorialText tutorialText)
    {
        Coroutine coroutine;
        Coroutine coroutineAddSize;
        Coroutine coroutineTakeSize;
        tutorialText.Tooltip.text = "";
        yield return Movement.Smooth(tutorialText.Object.transform, 0.25f, tutorialText.Object.transform.position, tutorialText.pos[0].transform.position); 
        coroutine = Main.Instance.StartCoroutine(tutorialText.SetText("IS YOUR HAND"));
        while (tutorialText.skip == false)
        {
            yield return null;
        }
        Main.Instance.StopCoroutine(coroutine);
        tutorialText.skip = false;
        yield return new WaitForSeconds(0.5f);
        coroutineTakeSize = Main.Instance.StartCoroutine(Movement.TakeSize(tutorialText.Panel.transform, 0.5f, 1, 0.5f));
        yield return Movement.Smooth(tutorialText.Object.transform, 0.25f, tutorialText.Object.transform.position, tutorialText.pos[1].transform.position); 
        coroutine = Main.Instance.StartCoroutine(tutorialText.SetText("IS YOUR DECK"));
        while (tutorialText.skip == false)
        {
            yield return null;
        }
        tutorialText.Panel.transform.localScale = new Vector2(0.5f, 1);
        Main.Instance.StopCoroutine(coroutine);
        Main.Instance.StopCoroutine(coroutineTakeSize);
        tutorialText.skip = false;
        yield return new WaitForSeconds(0.5f);
        yield return Movement.Smooth(tutorialText.Object.transform, 0.25f, tutorialText.Object.transform.position, tutorialText.pos[2].transform.position + Vector3.right / 3); 
        coroutine = Main.Instance.StartCoroutine(tutorialText.SetText("IS YOUR MANA"));
        while (tutorialText.skip == false)
        {
            yield return null;
        }
        Main.Instance.StopCoroutine(coroutine);
        tutorialText.skip = false;
        yield return new WaitForSeconds(1f);
        tutorialText.DisablePanel();
        yield return new WaitForSeconds(0.5f);
        tutorialText.Panel.enabled = false;
        tutorialText.Tooltip.transform.SetParent(tutorialText.Panel.transform);
        RectTransform tooltip = tutorialText.Tooltip.GetComponent<RectTransform>();
        tooltip.offsetMax = Vector2.zero;
        tooltip.offsetMin = Vector2.zero;
        tutorialText.Tooltip.text = "";
        tutorialText.Panel.transform.position = new Vector3(0f, 1f, 0);
        tutorialText.Tooltip.color = Color.black;
        Sounds.PlaySound(Sounds.Get<SoundPoof>(), 0.3f);
        GameObject pawn = Object.Instantiate(PrefabUtil.Load("Pawn"), new Vector2(0f, 1f), Quaternion.identity, Main.Instance.GUI);
        Object.Instantiate(PrefabUtil.Load("AppearEffect"), new Vector2(0f, 1f), Quaternion.identity, Main.Instance.GUI);
        tutorialText.Tooltip.transform.SetParent(tutorialText.Tooltip.transform.parent.parent);
        yield return Movement.TakeSize(tutorialText.Panel.transform, 0.5f, 0.4f, 1);
        yield return Movement.Smooth(pawn.transform, 0.25f, pawn.transform.position, pawn.transform.position + Vector3.left * 1.35f + Vector3.up / 2); 
        tutorialText.EnablePanel();
        coroutine = Main.Instance.StartCoroutine(tutorialText.SetText("Hiii!"));
        while (tutorialText.skip == false)
        {
            yield return null;
        }
        Main.Instance.StopCoroutine(coroutine);
        tutorialText.skip = false;
        yield return new WaitForSeconds(0.5f);
        coroutineAddSize = Main.Instance.StartCoroutine(Movement.AddSize(tutorialText.Panel.transform, 1, 1, 1));
        coroutine = Main.Instance.StartCoroutine(tutorialText.SetText("Let's talk about the rules!"));
        Coroutine coroutinePawn;
        coroutinePawn = Main.Instance.StartCoroutine(Movement.Smooth(pawn.transform, 1f, pawn.transform.position, pawn.transform.position + Vector3.left / 1.2f + Vector3.up / 1.35f));
        while (tutorialText.skip == false)
        {
            yield return null;
        }
        Main.Instance.StopCoroutine(coroutine);
        Main.Instance.StopCoroutine(coroutineAddSize);
        Main.Instance.StopCoroutine(coroutinePawn);
        tutorialText.Panel.transform.localScale = new Vector2(1f, 1);
        pawn.transform.position = pawn.transform.position + Vector3.left / 1.2f + Vector3.up / 1.35f;
        tutorialText.skip = false;
        yield return new WaitForSeconds(0.5f);
        coroutine = Main.Instance.StartCoroutine(tutorialText.SetText("To win, you need"));
        while (tutorialText.skip == false)
        {
            yield return null;
        }
        Main.Instance.StopCoroutine(coroutine);
        tutorialText.skip = false;
        yield return new WaitForSeconds(0.5f);
        coroutine = Main.Instance.StartCoroutine(tutorialText.SetText("Eat all the opponent's pieces."));
        while (tutorialText.skip == false)
        {
            yield return null;
        }
        Main.Instance.StopCoroutine(coroutine);
        tutorialText.skip = false;
        yield return new WaitForSeconds(0.5f);
        coroutine = Main.Instance.StartCoroutine(tutorialText.SetText("That seems to be it"));
        while (tutorialText.skip == false)
        {
            yield return null;
        }
        Main.Instance.StopCoroutine(coroutine);
        tutorialText.skip = false;
        yield return new WaitForSeconds(0.5f);
        coroutine = Main.Instance.StartCoroutine(tutorialText.SetText("Oh, right"));
        while (tutorialText.skip == false)
        {
            yield return null;
        }
        Main.Instance.StopCoroutine(coroutine);
        tutorialText.skip = false;
        yield return new WaitForSeconds(0.5f);
        coroutineAddSize = Main.Instance.StartCoroutine(Movement.AddSize(tutorialText.Panel.transform, 1.25f, 1.25f, 1));
        coroutine = Main.Instance.StartCoroutine(tutorialText.SetText("An exposed piece (except for a pawn) can only move on the second move"));
        coroutinePawn = Main.Instance.StartCoroutine(Movement.Smooth(pawn.transform, 1f, pawn.transform.position, pawn.transform.position + Vector3.left / 2 + Vector3.up / 3f));
        while(tutorialText.skip == false)
        {
            yield return null;
        }
        Main.Instance.StopCoroutine(coroutine);
        Main.Instance.StopCoroutine(coroutineAddSize);
        Main.Instance.StopCoroutine(coroutinePawn);
        tutorialText.Panel.transform.localScale = new Vector2(1.25f, 1.25f);
        pawn.transform.position = pawn.transform.position + Vector3.left / 2 + Vector3.up / 3f;
        tutorialText.skip = false;
        Main.Instance.StartCoroutine(Movement.TakeSize(tutorialText.Panel.transform, 0.7f, 0.7f, 1));
        tutorialText.Tooltip.text = "";
        yield return Movement.Smooth(pawn.transform, 1f, pawn.transform.position, pawn.transform.position - Vector3.left  - Vector3.up);
        yield return tutorialText.SetText("Good luck!");
        yield return Movement.TakeOpacity(tutorialText.Panel.GetComponent<Image>(), 1, 2);
        tutorialText.DisablePanel();
        yield return Movement.Smooth(pawn.transform, 1f, pawn.transform.position, new Vector2(0f, 1f));//7
        yield return new WaitForSeconds(0.2f);
        Sounds.PlaySound(Sounds.Get<SoundPoof>(), 0.3f);
        Main.Instance.StartCoroutine(Movement.TakeOpacity(tutorialText.Tooltip, 1, 1));
        Object.Instantiate(PrefabUtil.Load("AppearEffect"), new Vector2(0f, 1f), Quaternion.identity, Main.Instance.GUI);
        Object.Destroy(pawn);

    }
}
