using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : Sounds
{
    public AudioClip AudioExposeFigure;
    public AudioClip AudioWin;
    public AudioClip AudioLose;
    public DeckData DeckData;
    public Factory Factory;
    public Characteristics Characteristics;
    public Board Board;
    public Hand Hand;
    public GameObject HintPanel;
    public Transform GUI;
    public Transform GUIEndPoint;
    public Transform BoardEndPoint;
    public Transform BoardStart;
    public Transform GUIStart;
    public Transform RivalHand;
    public GameObject Win;
    public GameObject Lose;
    public TutorialText TutorialText;
    public static Main Instance { get; private set; }
    public static int indexLevel = 0;
    public List<Level> Levels = new();

    private void Start()
    {
        /*        TutorialText.EnablePanel();
                StartCoroutine(Tutorial.Enable(TutorialText));*/
        Instance = this;
        Factory = new Factory();
        DeckData.GiveKitDefault(Factory);        
        StartCoroutine(DeckData.GiveFigure(this, Sound, Factory.Figure("w_pawn")));
        Board.DisableDragFigure();
        Levels.Add(new Level1());
        Levels.Add(new Level2());
        Levels[indexLevel].Init();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            int index = Random.Range(0, Factory.FigureCreators.Count);
            DeckData.AddToDeck(Factory.FigureCreators[index]);
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            DeckData.SpawnFigure();
        }

    }
    public void StartEndTurn()
    {
        StartCoroutine(EndTurn());

    }
    private bool _tryEndTurn = true;

    private IEnumerator EndTurn()
    {
        if (_tryEndTurn)
        {
            _tryEndTurn = false;

            yield return Movement.Smooth(GUI, 0.25f, GUI.position, GUIEndPoint.position);
            yield return Movement.Smooth(Board.transform, 0.25f, Board.transform.position, BoardEndPoint.position);
            yield return Movement.AddSmooth(Board.transform, 1, 1.3f, 1.5f);
            yield return new WaitForSeconds(0.25f);
            foreach (var s in Hand.Slots)
            {
                s.Drag.TryDrag = false;
            }
            Board.EnableDragFigure();

            if (Hand.DisplayedSlot.Count == 0)
                yield return Back();
        }
    }
    public IEnumerator Back()
    {
        Board.DisableFirstTurn();
        if (Hand.Slots.Count > 0 || DeckData.Figures.Count > 0)
        {
            Board.DisableDragFigure();
            yield return new WaitForSeconds(0.25f);
            yield return Movement.TakeSmooth(Board.transform, 1.3f, 1, 1.5f);
            yield return Movement.Smooth(Board.transform, 0.25f, BoardEndPoint.position, BoardStart.position);
            yield return Movement.Smooth(GUI, 0.25f, GUI.position, GUIStart.position);

            yield return new WaitForSeconds(0.3f);
            yield return DeckData.GiveFigure(this, Sound);

            if (Hand.Slots.Count > 0)
            {
                Characteristics.Mana = Characteristics.MaxMana;
                Characteristics.Amount.text = Characteristics.Mana.ToString() + "/" + Characteristics.MaxMana.ToString();
            }

            foreach (var s in Hand.Slots)
            {
                s.Drag.TryDrag = true;
            }

            _tryEndTurn = true;
        }

        if (Levels[indexLevel].Rival.DisplayedSlot.Count == 0
            && Levels[indexLevel].Rival.Figure.Count == 0)
        {
            PlaySound(AudioWin, 1, 1);
            Win.SetActive(true);
        }
        else if (Hand.DisplayedSlot.Count == 0 && DeckData.Figures.Count == 0 && Hand.Slots.Count == 0)
        {
            PlaySound(AudioLose, 1, 1);
            Lose.SetActive(true);
        }

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
        SceneManager.LoadScene("Map");
    }
}
