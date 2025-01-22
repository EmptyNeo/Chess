using System.Collections;
using TMPro;
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
    public static IEnumerator AddSize(Transform transform, float targetX, float targetY, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); 
            float currentX = Mathf.Lerp(startScale.x, targetX, t);
            float currentY = Mathf.Lerp(startScale.y, targetY, t);
            transform.localScale = new Vector3(currentX, currentY, 0);
            yield return null;
        }
        transform.localScale = new Vector3(targetX, targetY, 0); 
    }

    public static IEnumerator TakeSize(Transform transform, float targetX, float targetY, float duration)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float currentX = Mathf.Lerp(startScale.x, targetX, t);
            float currentY = Mathf.Lerp(startScale.y, targetY, t);
            transform.localScale = new Vector3(currentX, currentY, 0);
            yield return null;
        }
        transform.localScale = new Vector3(targetX, targetY, 0);
    }
    public static IEnumerator TakeOpacity(Transform transform, Vector3 b, Image image, float opacity, float speed)
    {
        while (opacity > 0)
        {
            opacity -= Time.deltaTime * speed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
            transform.position = b;
            yield return null;
        }

    }
    public static IEnumerator TakeOpacity(Image image, float opacity, float speed)
    {
        while (opacity > 0)
        {
            opacity -= Time.deltaTime * speed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
            yield return null;
        }

    }
    public static IEnumerator TakeOpacity(TMP_Text image, float opacity, float speed)
    {
        while (opacity > 0)
        {
            opacity -= Time.deltaTime * speed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
            yield return null;
        }

    }
    public static IEnumerator TakeOpacity(ParticleSystem particleSystem, float opacity, float speed)
    {
        ParticleSystem.MainModule main = particleSystem.main;
        while (opacity > 0)
        {
            opacity -= Time.deltaTime * speed;
            main.startColor = new Color(main.startColor.color.r, main.startColor.color.g, main.startColor.color.b, opacity);
            yield return null;
        }

    }
    public static IEnumerator TakeOpacity(Transform transform, Vector3 b, TMP_Text image, float opacity, float speed)
    {
        while (opacity > 0)
        {
            opacity -= Time.deltaTime * speed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
            transform.position = b;
            yield return null;
        }

    }
    public static IEnumerator AddOpacity(Transform transform, Vector3 b, Image image, float opacity, float maxOpacity, float speed)
    {

        while (opacity < maxOpacity)
        {
            opacity += Time.deltaTime * speed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, opacity);
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
