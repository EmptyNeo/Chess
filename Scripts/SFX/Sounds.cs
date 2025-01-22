using System.Collections;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static AudioSource[] AudioSource;
    private void Awake()
    {
        AudioSource = GetComponents<AudioSource>();
    }

    public static AudioSource PlaySound(AudioClip clip, float volume = 1, float p = 0.85f)
    {
        for (int i = 0; i < AudioSource.Length; i++)
        {
            if (AudioSource[i].clip == null)
            {
                AudioSource[i].clip = clip;
                AudioSource[i].pitch = p;
                AudioSource[i].PlayOneShot(clip, volume);
                AudioSource[i].clip = null;
                return AudioSource[i];
               
            }
        }
        return null;
    }
    public static IEnumerator GraduallyReducingVolume(AudioSource audioSource)
    {
       float startVolume = audioSource.volume;
       for (int i = 0; i < 300; i++)
       {
            audioSource.volume -= 0.01f;
            yield return null;
       }
        audioSource.volume = startVolume;
    }
    public static AudioClip Get<T>() where T : SoundFX, new() => new T().Clip;
}
