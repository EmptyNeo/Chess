using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{

    public Transform Player;
    public Animator PlayerAnimator;
    public List<Transform> Point = new();
    private void Start()
    {
        if (PlayerPrefs.HasKey("x") && PlayerPrefs.HasKey("y"))
        {
            Player.transform.position = new Vector2(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"));
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            PlayerPrefs.DeleteAll();
    }
    public void OnStart(int index)
    {
        if (Main.indexLevel == index)
            return;
        Main.indexLevel = index;
        Debug.Log(Vector2.Distance(Player.position, Point[index].position));
        if (Vector2.Distance(Player.position, Point[index].position) < 3f)
        {
            StartCoroutine(ChoiceLevel(index));
        }
    }
    public IEnumerator ChoiceLevel(int index)
    {
        PlayerAnimator.SetBool("walk", true);
        yield return Movement.Smooth(Player, 1, Player.position, Point[index].position + new Vector3(0, 0.5f,0));
        PlayerAnimator.SetBool("walk", false);
        PlayerPrefs.SetFloat("x", Player.position.x);
        PlayerPrefs.SetFloat("y", Player.position.y);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }
}
