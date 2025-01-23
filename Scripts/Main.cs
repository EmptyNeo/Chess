using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : Sounds
{
    public Deck DeckData;
    public TransformationFigure TransformationFigure;
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
    public GameObject Draw;
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
    public Transition Transition;
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
        new Level8(),
        new Level9(),
    };
    private void Start()
    {
        StartCoroutine(StartOn());

    }
    private IEnumerator StartOn()
    {
        _tryEndTurn = false;
        Transition.gameObject.SetActive(true);
        StartCoroutine(Transition.TakeOpacity(0.5f));

        Instance = this;
        Factory = new Factory();
        TransformationFigure.Init();
        DeckData.GiveDefaultDeck(Factory);
        DataDeck deck = BinarySavingSystem.LoadDeck();
        DeckData.GiveDeckCards(deck, Factory);
        if (PlayerPrefs.HasKey("IndexLevel"))
        {
            IndexLevel = PlayerPrefs.GetInt("IndexLevel");
        }
        Levels[IndexLevel].Init();

        if (Ivent != null)
            yield return Ivent.StartEvent();

        yield return new WaitForSeconds(1);
        yield return DeckData.GiveFigure(Get<SoundGiveCard>(), Factory.GetFigure<Pawn>(TypeFigure.White));
        for (int i = 0; i < Hand.Slots.Count; i++)
        {
            Hand.Slots[i].Drag.Image.raycastTarget = false;
        }
        yield return new WaitForSeconds(0.5f);
       
        if (IndexLevel == 0 && !PlayerPrefs.HasKey("tutorial") && TutorialText.gameObject.activeSelf)
        {
            TutorialText.EnablePanel();
            yield return Tutorial.Enable(TutorialText);
        }
        for (int i = 0; i < Hand.Slots.Count; i++)
        {
            Hand.Slots[i].Drag.Image.raycastTarget = true;
        }
        _tryEndTurn = true;
        IsCanMove = false;
    }
#if UNITY_EDITOR
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
            {
                DeckData.SpawnFigure();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            PlaySound(Get<SoundWin>(), 0.2f, 1);
            Win.SetActive(true);
        }
    }
#endif
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

            if (Hand.DisplayedSlot.Count > 0)
            {
                if (IsPossibleMove() == false || Hand.IsOnlySpecialCard())
                {
                    yield return Back();
                    yield break;
                }
            }
            else if (Hand.DisplayedSlot.Count == 0)
            {
                yield return Back();
                yield break;
            }
            IsCanMove = true;
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
        yield return Levels[IndexLevel].Rival.Attack();
        if (Levels[IndexLevel].Rival.IsPossibleAttack == false)
            yield return Levels[IndexLevel].Rival.RandomMove();

        foreach (Slot slot in Board.Slots)
        {
            if (slot.CardData.NotNull)
            {
                slot.CardData.LimitMove--;
                if (slot.CardData is FigureData figure)
                {
                    if (figure.LimitMove == 0 && figure.Icon.name == figure.NameSprite)
                        slot.FigureImage.sprite = SpriteUtil.Load("pieces", figure.NameSprite);
                }
            }
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
            Ivent.gameObject.SetActive(false);
        }
        else if (Hand.DisplayedSlot.Count == 0 && DeckData.Cards.Count == 0 && Hand.IsOnlySpecialCardInHand())
        {
            PlaySound(Get<SoundLose>(), 0.2f, 1);
            Lose.SetActive(true);
            Levels[IndexLevel].Rival = null;
            Ivent.gameObject.SetActive(false);
        }
        else if(Board.TryPossibleMove(Hand.DisplayedSlot) == false && Board.TryPossibleMove(Levels[IndexLevel].Rival.DisplayedSlot) == false)
        {
            PlaySound(Get<SoundDraw>(), 0.2f, 1);
            Draw.SetActive(true);
            Levels[IndexLevel].Rival = null;
            Ivent.gameObject.SetActive(false);
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
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < Hand.Slots.Count; i++)
            {
                Hand.Slots[i].Drag.Image.raycastTarget = true;
            }
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
        StartCoroutine(RestartOn());
    }
    public IEnumerator RestartOn()
    {
        PlayerPrefs.SetInt("IndexLevel", 1);
        PlayerPrefs.SetInt("Index", 1);
        PlayerPrefs.Save();
        yield return Transition.AddOpacity(1);
        SceneManager.LoadScene("Game");
    }
    public void RestartDraw()
    {
        StartCoroutine(RestartDrawOn());
    }
    public IEnumerator RestartDrawOn()
    {
        yield return Transition.AddOpacity(1);
        SceneManager.LoadScene("Game");
    }
    public void Continue()
    {
        StartCoroutine(ContinueOn());
    }
    public IEnumerator ContinueOn()
    {
        if (IndexLevel < Levels.Count - 1)
        {
            Win.SetActive(false);
            Pick.SetActive(true);
            GiveRandomCardToDeck();
        }
        else
        {
            yield return Transition.AddOpacity(1f);
            SceneManager.LoadScene("Victory");
            BinarySavingSystem.DeleteDeck();
            PlayerPrefs.DeleteAll();
        }
    }
    public void Skip()
    {
        StartCoroutine(SkipOn());
    }
    public IEnumerator SkipOn()
    {
        yield return Transition.AddOpacity(1f);
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
                creators.Add(f);
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
