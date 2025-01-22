using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FogEvent : Event
{
    public GameObject Content;
    public override IEnumerator StartEvent()
    {
        Panel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Image image = Panel.GetComponent<Image>();
        TMP_Text imageText = Panel.GetComponentInChildren<TMP_Text>();
        StartCoroutine(Movement.TakeOpacity(Panel.transform, Panel.transform.position, imageText, imageText.color.a, 0.5f));
        yield return Movement.TakeOpacity(Panel.transform, Panel.transform.position, image, image.color.a, 0.5f);
        Panel.SetActive(false);
        yield return new WaitForSeconds(1f);
        //появление тумана
        Content.SetActive(true);

        Content.transform.SetParent(Main.Instance.BoardParent.transform);
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            Content.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
