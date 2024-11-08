using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject Win;
    public GameObject Lose;
    public TutorialText TutorialText;
    public static Main Instance { get; private set; }
    private void Start()
    {
/*        TutorialText.EnablePanel();
        StartCoroutine(Tutorial.Enable(TutorialText));*/
        Instance = this;
        Factory = new Factory();
        Factory.Initialization(true);
        GiveKitDefault(DeckData, Factory);
        StartCoroutine(GiveFigure(DeckData, Factory.Figure("Pawn")));
        Board.DisableDragFigure();
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
            yield return GiveFigure(DeckData);

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

        //проверка на отсутствие выставленных фигур, отсутствие фигур в руке, отсутствие фигур в колоде
        //{
            //PlaySound(AudioWin, 1, 1);
            //Win.SetActive(true);
        //}
        if (Hand.DisplayedSlot.Count == 0 && DeckData.Figures.Count == 0 && Hand.Slots.Count == 0)
        {
            PlaySound(AudioLose, 1, 1);
            Lose.SetActive(true);
        }
       

    }

    private IEnumerator GiveFigure(DeckData deckData)
    {
        if (deckData.Figures.Count == 0)
            yield return null;

        for (int i = 0; i < 3; i++)
        {
            if (deckData.Figures.Count > 0)
            {
                PlaySound(Sound, 1, 1);
                deckData.SpawnFigure();
                yield return new WaitForSeconds(0.2f);
            }
        }

    }
    private IEnumerator GiveFigure(DeckData deckData, FigureData figureData)
    {
        if (deckData.Figures.Count == 0)
            yield return null;

        for (int i = 0; i < 3; i++)
        {
            if (deckData.Figures.Count > 0)
            {

                PlaySound(Sound, 1, 1);
                deckData.SpawnFigure(figureData);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
    private void GiveKitDefault(DeckData deckData, Factory factory)
    {
        for (int i = 0; i < 8; i++)
            deckData.AddToDeck(factory.Figure("Pawn"));
        for (int i = 0; i < 2; i++)
        {
            deckData.AddToDeck(factory.Figure("Bishop"));
            deckData.AddToDeck(factory.Figure("Knight"));
            deckData.AddToDeck(factory.Figure("Rook"));
        }
        deckData.AddToDeck(factory.Figure("Queen"));
    }
}
public static class Movement
{
    public static IEnumerator Smooth(Transform transform, float duration, Vector3 a, Vector3 b)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            transform.position = Vector2.Lerp(a, b, t);
            yield return null;
        }

    }
    public static IEnumerator TakeOpacity(Transform transform, Vector3 b,Image image, float duration, float speed)
    {
        float time = duration;

        while (time > 0)
        {
            time -= Time.deltaTime * speed;
            image.color = new Color(image.color.r, image.color.g, image.color.b, time);
            transform.position = b;
            yield return null;
        }

    }

    public static IEnumerator AddSmooth(Transform transform, float startScale, float maxScale, float speed)
    {
        float scale = startScale;
        Vector3 pos = transform.position;
        while (scale < maxScale)
        {
            scale += Time.deltaTime * speed;
            transform.localScale = new Vector2(scale, scale);
            transform.position = pos;
            yield return null;
        }
        transform.position = pos;
    }
    public static IEnumerator TakeSmooth(Transform transform, float startScale, float maxScale, float speed)
    {
        float scale = startScale;

        while (scale > maxScale)
        {
            scale -= Time.deltaTime * speed;
            transform.localScale = new Vector2(scale, scale);
            yield return null;
        }
        transform.localScale = new Vector2(maxScale, maxScale);
    }
}
