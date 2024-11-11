using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : Sounds
{
    public AudioClip AudioExposeFigure;
    public AudioClip AudioWin;
    public AudioClip AudioLose;
    public Deck DeckData;
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
    public Card Card;
    public Transform[] CardPoint;
    public static Main Instance { get; private set; }
    public static int indexLevel;
    public static List<Level> Levels = new()
    {
        new Level0(),
        new Level1(),
        new Level2(),
        new Level3(),
        new Level4(),
        new Level5(),
        new Level6(),
        new Level7(),
        new Level8()
    };
    private void Start()
    {

        /*        TutorialText.EnablePanel();
                StartCoroutine(Tutorial.Enable(TutorialText));*/
        Instance = this;
        Factory = new Factory();
        DeckData.GiveDefaultDeck(Factory);
        StartCoroutine(DeckData.GiveFigure(this, Sound, Factory.GetFigure("w_pawn")));

        DataDeck deck = BinarySavingSystem.LoadDeck();
        DeckData.GiveDeckCards(deck, Factory);
        Board.DisableDragFigure();
        Levels[indexLevel].Init();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            int index = Random.Range(0, Factory.Creators.Count);
            DeckData.AddToDeck(Factory.Creators[index]);
        }
        if (Input.GetKeyUp(KeyCode.V))
        {
            DeckData.SpawnFigure();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Win.SetActive(true);
            StartCoroutine(GiveRandomCardToDeck());
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
            yield return GiveRandomCardToDeck();
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
    public IEnumerator GiveRandomCardToDeck()
    {
        List<FigureData> creators = new();
        foreach (FigureData f in Factory.Creators)
        {
            if (f.ColorFigure == ColorFigure.White && f.Name != "w_pawn")
            {
                if (DeckData.NameFigures.Count + 8 < 16)
                    creators.Add(f);
                else
                    Win.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        if (creators.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Card card = Instantiate(Card.gameObject, CardPoint[0].transform.position, Quaternion.identity, Win.transform).GetComponent<Card>();
                int index = Random.Range(0, creators.Count);
                card.SetCard(creators[index]);
                creators.RemoveAt(index);
                yield return Movement.Smooth(card.transform, 0.25f, card.transform.position, CardPoint[i].position);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

}
