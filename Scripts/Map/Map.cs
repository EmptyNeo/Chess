using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    public Transform PlayerTransform;
    public ImageAnimation Player;
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
            Vector3 pos = Point[index].transform.position;
            Player.transform.position = new Vector3(pos.x, pos.y + 0.4f, 0);
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
        if (IndexLevel != Point[index].Index && Point[index].Index == IndexLevel + 1)
        {
            StartCoroutine(ChoiceLevel(index));
        }
    }
    public IEnumerator ChoiceLevel(int index)
    {
        Player.StateAnimation = StateAnimation.Walk;
        yield return Movement.Smooth(PlayerTransform.transform, 1, PlayerTransform.position, Point[index].transform.position + new Vector3(0, 0.4f, 0));
        Player.StateAnimation = StateAnimation.Idle;
        PlayerPrefs.SetInt("index", index);
        PlayerPrefs.SetInt("IndexLevel", Point[index].Index);
        PlayerPrefs.Save();
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Game");
    }
}
