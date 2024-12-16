using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
[Serializable]
public class Level
{
    public Rival Rival;
    public virtual void Init()
    {

    }
}
public class Level0 : Level
{
    public override void Init()
    {
        int maxLength = 4;
        int maxAmountX = 8;
        int maxAmountY = 2;
        List<CardData> figureDatas = new();
        for (int i = 0; i < maxLength; i++)
            figureDatas.Add(new Pawn(Random.Range(0, maxAmountX), Random.Range(0, maxAmountY), "b_pawn", TypeFigure.Black));

        Rival = new Rival(figureDatas);
    }
}
public class Level1 : Level
{
    public override void Init()
    {
        int maxLength = 4;
        int maxAmountX = 8;
        int maxAmountY = 2;
        List<CardData> figureDatas = new();
        List<CardData> deck = new();

        for (int i = 0; i < maxLength; i++)
            figureDatas.Add(new Pawn(Random.Range(0, maxAmountX), Random.Range(0, maxAmountY), "b_pawn", TypeFigure.Black));

        deck.Add(new Pawn(0, 0, "b_pawn", TypeFigure.Black));
        deck.Add(new Pawn(0, 0, "b_pawn", TypeFigure.Black));

        Rival = new Rival(figureDatas)
        {
            Deck = deck
        };
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Earthquake"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level2 : Level
{
    public override void Init()
    {
        Rival = new Rival(new List<CardData>()
        {
            new Pawn(2,1, "b_pawn", TypeFigure.Black),
            new Pawn(3,1, "b_pawn", TypeFigure.Black),
            new Pawn(4,1, "b_pawn", TypeFigure.Black),
            new Pawn(5,1, "b_pawn", TypeFigure.Black)
        });
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Earthquake"), Main.Instance.GUI).GetComponent<Event>();
       

    }
}
public class Level3: Level
{
    public override void Init()
    {
        int randomIvent = Random.Range(0, 2);
        List<CardData> figures = new();
        switch (randomIvent)
        {
            case 0:
                for (int i = 2; i < 6; i++)
                    figures.Add(new Hog(i % 6, 2, "b_hog", TypeFigure.Black));

                break;
            case 1:
                for (int i = 2; i < 6; i++)
                    figures.Add(new Wolf(i % 6, 2, "b_wolf", TypeFigure.Black));
                break;
        }
        Rival = new Rival(figures);

        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Earthquake"), Main.Instance.GUI).GetComponent<Event>();
    }
   
}
public class Level4 : Level
{
    public override void Init()
    {
        Rival = new Rival(new List<CardData>()
        {
            new Knight(1,1, "b_knight", TypeFigure.Black),
            new Knight(6,1, "b_knight", TypeFigure.Black),
            new Pawn(1,2, "b_pawn", TypeFigure.Black),
            new Pawn(2,2, "b_pawn", TypeFigure.Black),
            new Pawn(3,2, "b_pawn", TypeFigure.Black),
            new Pawn(4,2, "b_pawn", TypeFigure.Black),
            new Pawn(5,2, "b_pawn", TypeFigure.Black),
            new Pawn(6,2, "b_pawn", TypeFigure.Black)
        });
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Winter"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level5 : Level
{
    public override void Init()
    {
        Rival = new Rival(new List<CardData>()
        {
            new Pawn(2,1, "b_pawn", TypeFigure.Black),
            new Pawn(3,1, "b_pawn", TypeFigure.Black),
            new Pawn(4,1, "b_pawn", TypeFigure.Black),
            new Pawn(5,1, "b_pawn", TypeFigure.Black)
        });

        Main.Instance.Ivent =  Object.Instantiate(PrefabUtil.Load("Winter"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level6 : Level
{
    public override void Init()
    {
        int randomIvent = Random.Range(0, 2);
        List<CardData> figures = new();
        switch (randomIvent)
        {
            case 0:
                figures.Add(new Hog(2, 2, "b_hog", TypeFigure.Black));
                figures.Add(new Hog(3, 2, "b_hog", TypeFigure.Black));
                figures.Add(new Hog(4, 2, "b_hog", TypeFigure.Black));
                figures.Add(new Hog(5, 2, "b_hog", TypeFigure.Black));
                break;
            case 1:
                figures.Add(new Wolf(2, 2, "b_wolf", TypeFigure.Black));
                figures.Add(new Wolf(3, 2, "b_wolf", TypeFigure.Black));
                figures.Add(new Wolf(4, 2, "b_wolf", TypeFigure.Black));
                figures.Add(new Wolf(5, 2, "b_wolf", TypeFigure.Black));
                break;
        }
        Rival = new Rival(figures);

        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Smoke"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level7 : Level
{
    public override void Init()
    {
        int randomIvent = Random.Range(0, 2);
        List<CardData> figures = new();
        List<CardData> deck = new();
        switch (randomIvent)
        {
            case 0:
                for (int i = 2; i < 6; i++)
                    figures.Add(new Hog(i, 2, "b_hog", TypeFigure.Black));

                deck.Add(new Hog(0, 0, "b_hog", TypeFigure.Black));
                deck.Add(new Hog(0, 0, "b_hog", TypeFigure.Black));
                deck.Add(new Hog(0, 0, "b_hog", TypeFigure.Black));

                break;
            case 1:
                for (int i = 2; i < 6; i++)
                    figures.Add(new Wolf(i, 2, "b_wolf", TypeFigure.Black));

                deck.Add(new Wolf(0, 0, "b_wolf", TypeFigure.Black));
                deck.Add(new Wolf(0, 0, "b_wolf", TypeFigure.Black));
                deck.Add(new Wolf(0, 0, "b_wolf", TypeFigure.Black));

                break;
        }
        Rival = new Rival(figures)
        {
            Deck = deck
        };
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Smoke"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level8 : Level
{
    public override void Init()
    {
        Rival = new Rival(new List<CardData>()
        {
            new Pawn(2,1, "b_pawn", TypeFigure.Black),
            new Pawn(3,1, "b_pawn", TypeFigure.Black),
            new Pawn(4,1, "b_pawn", TypeFigure.Black),
            new Pawn(5,1, "b_pawn", TypeFigure.Black)
        });
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Treason"), Main.Instance.GUI.parent).GetComponent<Event>();
    }
}
