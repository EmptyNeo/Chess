using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : Sounds
{
    public Transition Transition;
    public void OnClick()
    {
        StartCoroutine(Click());
    }
    public IEnumerator Click()
    {
        PlaySound(Get<SoundClick>(), 1, 1f);
        yield return Transition.AddOpacity(1);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }
}
