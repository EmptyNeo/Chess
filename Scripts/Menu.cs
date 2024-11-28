using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : Sounds
{
    public void OnClick()
    {
        StartCoroutine(Click());
    }
    public IEnumerator Click()
    {
        PlaySound(Get<SoundClick>(), 1, 1f);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Game");
    }
}
