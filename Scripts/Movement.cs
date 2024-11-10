using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Movement
{
    public static IEnumerator Smooth(Transform transform, float duration, Vector3 a, Vector3 b)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            transform.position = Vector2.Lerp(a, b, t);
            yield return null;
        }

    }
    public static IEnumerator TakeOpacity(Transform transform, Vector3 b,Image image, float duration, float speed)
    {
        float time = duration;

        while (time > 0)
        {
            time -= Time.deltaTime * speed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, time);
            transform.position = b;
            yield return null;
        }

    }

    public static IEnumerator AddSmooth(Transform transform, float startScale, float maxScale, float speed)
    {
        float scale = startScale;
        Vector3 pos = transform.position;
        while (scale < maxScale)
        {
            scale += Time.deltaTime * speed;
            transform.localScale = new Vector2(scale, scale);
            transform.position = pos;
            yield return null;
        }
        transform.position = pos;
    }
    public static IEnumerator TakeSmooth(Transform transform, float startScale, float maxScale, float speed)
    {
        float scale = startScale;

        while (scale > maxScale)
        {
            scale -= Time.deltaTime * speed;
            transform.localScale = new Vector2(scale, scale);
            yield return null;
        }
        transform.localScale = new Vector2(maxScale, maxScale);
    }
}
