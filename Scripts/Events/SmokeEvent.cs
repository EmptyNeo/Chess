using System.Collections;
using UnityEngine;

public class SmokeEvent : Event
{
    public GameObject Content;
    public override IEnumerator StartEvent()
    {
        Panel.SetActive(true);
        yield return new WaitForSeconds(2f);
        Panel.SetActive(false);
        //появление тумана
        Content.SetActive(true);
        Vector2 pos = Content.transform.position;
        Content.transform.SetParent(Main.Instance.BoardParent.transform);
        for(int i = 0; i < 5; i++)
            Content.transform.position += Vector3.up;
        yield return Movement.Smooth(Content.transform, 1f, Content.transform.position, pos);
        yield return new WaitForSeconds(0.5f);
        
        Destroy(gameObject);
    }
}
