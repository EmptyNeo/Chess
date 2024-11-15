using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{

    public Transform Player;
    public Animator PlayerAnimator;
    public List<MapPoint> Point = new();
    public int IndexLevel;
    private void Start()
    {
        if (PlayerPrefs.HasKey("IndexLevel"))
        {
            IndexLevel = PlayerPrefs.GetInt("IndexLevel");
        }
        if (PlayerPrefs.HasKey("index"))
        {
            int index = PlayerPrefs.GetInt("index");
            if (IndexLevel < Point[^1].Index)
            {
                Vector3 pos = Point[index].transform.position;
                Player.transform.position = new Vector3(pos.x, pos.y + 0.5f, 0);
            }
            else
            {
                SceneManager.LoadScene("Victory");
                BinarySavingSystem.DeleteDeck();
                PlayerPrefs.DeleteAll();
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
        Debug.Log(Main.Instance.IndexLevel);
        if (IndexLevel != Point[index].Index && Point[index].Index == IndexLevel + 1)
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
        PlayerPrefs.SetInt("IndexLevel", Point[index].Index);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Game");
    }
}
