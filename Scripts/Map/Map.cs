using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{

    public Transform Player;
    public Animator PlayerAnimator;
    public List<MapPoint> Point = new();
    private void Start()
    {

        if (PlayerPrefs.HasKey("index"))
        {
            int index = PlayerPrefs.GetInt("index");
            if (Main.indexLevel < Point[^1].Index)
            {
                Vector3 pos = Point[index].transform.position;
                Player.transform.position = new Vector3(pos.x, pos.y + 0.5f, 0);
            }
            else
            {
                SceneManager.LoadScene("Victory");
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.DeleteAll();
            BinarySavingSystem.DeleteDeck();
        }
    }
    public void OnStart(int index)
    {
        if (Main.indexLevel != index && Point[index].Index == Main.indexLevel + 1)
        {
            StartCoroutine(ChoiceLevel(index));
        }
    }
    public IEnumerator ChoiceLevel(int index)
    {
        PlayerAnimator.SetBool("walk", true);
        yield return Movement.Smooth(Player, 1, Player.position, Point[index].transform.position + new Vector3(0, 0.5f,0));
        PlayerAnimator.SetBool("walk", false);
        PlayerPrefs.SetInt("index", index);
        PlayerPrefs.Save();
        Main.indexLevel = Point[index].Index;
        SceneManager.LoadScene("Game");
    }
}
