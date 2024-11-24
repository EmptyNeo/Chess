using System.Collections;
using UnityEngine;

public class ShakeUtil : MonoBehaviour
{
    public static ShakeUtil Instance { get; private set; }
    public void Start()
    {
        Instance = this;
    }
    private Vector3 originalPosition;
    public IEnumerator Shake(float duration,float intensity)
    {
        Main.Instance.Canvas.renderMode = RenderMode.WorldSpace;
        originalPosition = transform.localPosition;
        InvokeRepeating(nameof(DoShake), 0, intensity);
        Invoke(nameof(StopShake), duration);
        yield return new WaitForSeconds(duration);
        Main.Instance.Canvas.renderMode = RenderMode.ScreenSpaceCamera;
        yield return null;
    }

    public IEnumerator Shake(float intensity)
    {

        originalPosition = transform.localPosition;
        InvokeRepeating(nameof(DoShake), 0, intensity);

        yield return null;
    }

    private void DoShake()
    {
        float offsetX = Random.Range(-0.1f, 0.1f);
        float offsetY = Random.Range(-0.1f, 0.1f);
        transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);
    }

    public void StopShake()
    {
        CancelInvoke(nameof(DoShake));
        transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, -10);
    }
}