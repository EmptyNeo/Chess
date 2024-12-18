using System.Collections;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static AudioSource AudioSource;
    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip clip, float volume = 1, float p = 0.85f)
    {
        AudioSource.clip = clip;
        AudioSource.pitch = p;
        AudioSource.PlayOneShot(clip, volume);
    }
    public static IEnumerator GraduallyReducingVolume()
    {
       float startVolume = AudioSource.volume;
       for (int i = 0; i < 300; i++)
       {
            AudioSource.volume -= 0.01f;
            yield return null;
       }
       AudioSource.volume = startVolume;
    }
    public static AudioClip Get<T>() where T : SoundFX, new() => new T().Clip;
}
