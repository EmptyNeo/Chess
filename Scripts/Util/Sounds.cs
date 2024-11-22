using UnityEngine;

public class Sounds : MonoBehaviour
{
    //sound = give_card
    //expose card = expose_card
    //expose barrel = expose_barrel 
    //take card = take_card
    //win = win
    //lose = lose
    //freezing = freezing
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
    public static AudioClip Get(string name)
    {
        return Resources.Load<AudioClip>("Sounds/" + name);
    }
}
