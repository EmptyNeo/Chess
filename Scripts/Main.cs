using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : Sounds
{
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
    public Event Ivent;
    public Tooltip Tooltip;
    public bool IsCanMove;
    public static Main Instance { get; private set; }
    public int IndexLevel;
    public Canvas Canvas;
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
        StartCoroutine(DeckData.GiveFigure(Get<SoundGiveCard>(), Factory.GetFigure<Pawn>(TypeFigure.White)));
        DataDeck deck = BinarySavingSystem.LoadDeck();
        DeckData.GiveDeckCards(deck, Factory);
        if (PlayerPrefs.HasKey("IndexLevel"))
        {
            IndexLevel = PlayerPrefs.GetInt("IndexLevel");
        }
        Levels[IndexLevel].Init();
        if (Ivent != null)
            StartCoroutine(Ivent.StartEvent());
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
            PlaySound(Get<SoundWin>(), 0.2f, 1);
            Win.SetActive(true);
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
            IsCanMove = true;
            if (Hand.DisplayedSlot.Count > 0)
            {

                if (IsPossibleMove() == false || Hand.IsOnlySpecialCard())
                {
                    yield return Back();
                }
            }
            else if (Hand.DisplayedSlot.Count == 0)
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
        yield return Levels[IndexLevel].Rival.Move();
        foreach (Slot slot in Board.Slots)
        {
            if (slot.CardData.NotNull)
                slot.CardData.LimitMove--;
        }
        foreach (Slot slot in Board.Slots)
        {
            if (slot.CardData is FigureData figure && figure.IsProtected)
                figure.IsProtected = false;
        }
        yield return new WaitForSeconds(0.15f);
        if (Levels[IndexLevel].Rival.DisplayedSlot.Count == 0
            && Levels[IndexLevel].Rival.Deck.Count == 0)
        {
            PlaySound(Get<SoundWin>(), 0.2f, 1);
            Win.SetActive(true);
        }
        else if (Hand.DisplayedSlot.Count == 0 && DeckData.Cards.Count == 0 && Hand.Slots.Count == 0)
        {
            PlaySound(Get<SoundLose>(), 0.2f, 1);
            Lose.SetActive(true);
        }
        else
        {
            if (Ivent != null)
                yield return Ivent.StartEvent();
        }
        if (Hand.Slots.Count > 0 || DeckData.Cards.Count > 0)
        {
            for (int i = 0; i < Hand.DisplayedSlot.Count; i++)
            {
                StartCoroutine(Hand.DisplayedSlot[i].DragSlot.ReturnToSlot());
                Board.Instance.HideHints();
                Board.Instance.HideBacklight();
                Hand.DisplayedSlot[i].DragSlot.GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1f);
                Hand.DisplayedSlot[i].DragSlot.GetComponentInChildren<Image>().raycastTarget = true;
                Hand.DisplayedSlot[i].DragSlot.GetComponentInChildren<RectTransform>().localScale = new Vector2(1f, 1f);
            }
            yield return new WaitForSeconds(0.25f);
            yield return Movement.TakeSmooth(BoardParent.transform, 1.3f, 1, 1.5f);
            yield return Movement.Smooth(BoardParent.transform, 0.25f, BoardEndPoint.position, BoardStart.position);
            yield return Movement.Smooth(GUI, 0.25f, GUI.position, GUIStart.position);

            yield return new WaitForSeconds(0.3f);
            yield return DeckData.GiveFigure(Get<SoundGiveCard>());

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

        else if (IsPossibleMove() == false || Hand.IsOnlySpecialCard())
        {
            yield return new WaitForSeconds(1f);
            yield return Back();
        }


    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Continue()
    {
        if (PlayerPrefs.HasKey("index"))
        {
            int index = PlayerPrefs.GetInt("index");
            if (index < Levels.Count)
            {
                Win.SetActive(false);
                Pick.SetActive(true);
                GiveRandomCardToDeck();
            }
            else
            {
                SceneManager.LoadScene("Victory");
                BinarySavingSystem.DeleteDeck();
                PlayerPrefs.DeleteAll();
            }
        }
        else
        {
            Win.SetActive(false);
            Pick.SetActive(true);
            GiveRandomCardToDeck();
        }
    }
    public void Skip()
    {
        SceneManager.LoadScene("Map");
    }
    public int AmountChoiceCard;
    public void GiveRandomCardToDeck()
    {
        List<CardData> creators = new();
        foreach (CardData f in Factory.Creators)
        {
            if (f.TypeFigure == TypeFigure.White || f is SpecialCard && f.NameSprite != "w_pawn")
            {
                if (DeckData.NameFigures.Count + 8 < 16)
                    creators.Add(f);
                else
                    Pick.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        
        foreach (string name in DeckData.NameFigures)
        {
            if (name == "w_queen")
            {
                creators.RemoveAll(f => f.NameSprite == "w_queen");
                break;
            }
        }
        int randomIndex = Random.Range(0, 4);
        if (creators.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                int index;
                Card card = Instantiate(Card.gameObject, CardPoint[i].transform.position, Quaternion.identity, Pick.transform).GetComponent<Card>();
                if (i == randomIndex)
                {
                    index = Random.Range(0, creators.Where(f => f.TypeFigure == TypeFigure.White).Count());
                }
                else
                    index = Random.Range(0, creators.Count);

                card.SetCard(creators[index]);
                creators.RemoveAt(index);
            }
        }
    }
    public void GiveRandomCardToDeck(List<CardData> specials)
    {
        if (specials.Count > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Card card = Instantiate(Card.gameObject, CardPoint[i].transform.position, Quaternion.identity, Pick.transform).GetComponent<Card>();
                int index = Random.Range(0, specials.Count);

                card.SetCard(specials[index]);
                specials.RemoveAt(index);
            }
        }
    }
}
