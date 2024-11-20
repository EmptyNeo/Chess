using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    public void Start()
    {
        Instance = this;
    }
    private Vector3 originalPosition;
    public IEnumerator ShakeCamera(float duration,float intensity)
    {
        originalPosition = transform.localPosition;
        InvokeRepeating(nameof(DoShake), 0, intensity);
        Invoke(nameof(StopShake), duration);
        yield return null;
    }

    public IEnumerator ShakeCamera(float intensity)
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
        transform.localPosition = originalPosition;
    }
}