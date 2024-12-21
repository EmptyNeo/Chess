using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public static Transition Instance { get; private set; }
    private SpriteRenderer _image => GetComponent<SpriteRenderer>();
    public void Start()
    {
        Instance = this;
    }
   
    public IEnumerator TakeOpacity(float speed)
    {
        float opacity = 1;
        while (opacity > 0)
        {
            opacity -= Time.deltaTime * speed;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, opacity);
            yield return null;
        }
    }
    public IEnumerator AddOpacity(float speed)
    {
        float opacity = 0;
        float duration = 1;
        while (opacity < duration)
        {
            opacity += Time.deltaTime * speed;
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, opacity);
            yield return null;
        }
    }
}