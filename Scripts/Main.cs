using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
    public GameObject Pick;
    public Transform BoardParent;
    public TutorialText TutorialText;
    public Card Card;
    public Transform[] CardPoint;
    public Ivent Ivent;
    public static Main Instance { get; private set; }
    public int IndexLevel;
    public static List<Level> Levels = new()
    {
        new Level0(),
        new Level1(),
        new Level2(),
        new Level3(),
        new Level4(),
        new Level5(),
        new Level6()
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
        if (PlayerPrefs.HasKey("IndexLevel"))
        {
            IndexLevel = PlayerPrefs.GetInt("IndexLevel");
        }
        Levels[IndexLevel].Init();
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
            if (DeckData.Cards.Count > 0)
                DeckData.SpawnFigure();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Pick.SetActive(true);
            GiveRandomCardToDeck();
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
            yield return Movement.Smooth(BoardParent.transform, 0.25f, BoardParent.transform.position, BoardEndPoint.position);
            yield return Movement.AddSmooth(BoardParent.transform, 1, 1.3f, 1.5f);
            yield return new WaitForSeconds(0.25f);
            foreach (var s in Hand.Slots)
            {
                s.Drag.TryDrag = false;
            }
            Board.EnableDragFigure();
            if (Hand.DisplayedSlot.Count > 0)
            {

                if (IsPossibleMove() == false)
                {
                    yield return Back();
                }
                if (Hand.IsOnlySpecialCard())
                    yield return Back();
            }
            if (Hand.DisplayedSlot.Count == 0)
                yield return Back();


        }
    }
    public bool IsPossibleMove()
    {
        bool[] isPossible = new bool[Hand.DisplayedSlot.Count];
        for (int i = 0; i < Hand.DisplayedSlot.Count; i++)
        {
            if (Hand.DisplayedSlot[i].CardData is FigureData figure)
            {
                isPossible[i] = Board.TryPossibleMove(figure) && figure.LimitMove == 0;
            }
        }
        for (int i = 0; i < isPossible.Length; i++)
        {
            if (isPossible[i] == true)
                return true;
        }
        return false;
    }
    public IEnumerator Back()
    {
        yield return new WaitForSeconds(0.15f);

        for (int i = 0; i < Hand.DisplayedSlot.Count; i++)
        {
            Hand.DisplayedSlot[i].CardData.LimitMove--;
        }

        Ivent.StartIvent();
        if (Hand.Slots.Count > 0 || DeckData.Cards.Count > 0)
        {
            Board.DisableDragFigure();
            yield return new WaitForSeconds(0.25f);
            yield return Movement.TakeSmooth(BoardParent.transform, 1.3f, 1, 1.5f);
            yield return Movement.Smooth(BoardParent.transform, 0.25f, BoardEndPoint.position, BoardStart.position);
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
        else if(IsPossibleMove() == false || Hand.IsOnlySpecialCard())
        {
            yield return Back();
        }
        if (Levels[IndexLevel].Rival.DisplayedSlot.Count == 0
            && Levels[IndexLevel].Rival.Figure.Count == 0)
        {
            PlaySound(AudioWin, 1, 1);
            Win.SetActive(true);
        }
        else if (Hand.DisplayedSlot.Count == 0 && DeckData.Cards.Count == 0 && Hand.Slots.Count == 0)
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
        Win.SetActive(false);
        Pick.SetActive(true);

        GiveRandomCardToDeck();
    }
    public void Skip()
    {
        SceneManager.LoadScene("Map");
    }
    public void GiveRandomCardToDeck()
    {
        List<CardData> creators = new();
        foreach (CardData f in Factory.Creators)
        {
            if ((f.TypeFigure == TypeFigure.White || f.TypeFigure == TypeFigure.Special) && f.Name != "w_pawn")
            {
                if (DeckData.NameFigures.Count + 8 < 16)
                    creators.Add(f);
                else
                    Pick.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        if (creators.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Card card = Instantiate(Card.gameObject, CardPoint[i].transform.position, Quaternion.identity, Pick.transform).GetComponent<Card>();
                int index = Random.Range(0, creators.Count);
                card.SetCard(creators[index]);
                creators.RemoveAt(index);
            }
        }
    }

}
