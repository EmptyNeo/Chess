using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : Sounds
{
    public Transform PlayerTransform;
    public ImageAnimation Player;
    public List<MapPoint> Point = new();
    public int Index ;
    public Transition Transition;
    private void Start()
    {
        Transition.gameObject.SetActive(true);
        StartCoroutine(Transition.TakeOpacity(0.5f));
        if (PlayerPrefs.HasKey("Index"))
        {
            Index = PlayerPrefs.GetInt("Index");
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
        if(Input.GetMouseButtonDown(0))
            PlaySound(Get<SoundClick>(), 1, 1f);
    }
    public void OnStart(int index)
    {
        if (Index != Point[index].Index && Point[index].Index == Index + 1)
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
        PlayerPrefs.SetInt("IndexLevel", Point[index].IndexLevel);
        PlayerPrefs.SetInt("Index", Point[index].Index);
        PlayerPrefs.Save();
        yield return Transition.AddOpacity(2f);
        SceneManager.LoadScene("Game");
    }
}
