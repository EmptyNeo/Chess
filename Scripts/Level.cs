using System;
using System.Collections.Generic;
using System.Linq;
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
        HashSet<Vector2Int> coordinates = new ();
        
        int maxLength = 4;
        int maxAmountX = 8;
        int maxAmountY = 3;

        List<CardData> figureDatas = new();
        
        for (int i = 0; i < maxLength; i++)
        {
            int x = Random.Range(0, maxAmountX);
            int y = Random.Range(0, maxAmountY);
            coordinates.Add(new Vector2Int(x, y));
        }
        foreach(Vector2Int coor in coordinates)
        {
            figureDatas.Add(new Pawn(coor.x, coor.y, "b_pawn", TypeFigure.Black));
        }
        Rival = new Rival(figureDatas);
    }
}
public class Level1 : Level
{
    public override void Init()
    {
        HashSet<Vector2Int> coordinates = new();
        int maxLength = 4;
        int maxAmountX = 8;
        int maxAmountY = 2;
        List<CardData> figureDatas = new();
        List<CardData> deck = new();
        for (int i = 0; i < maxLength; i++)
        {
            int x = Random.Range(0, maxAmountX);
            int y = Random.Range(0, maxAmountY);
            coordinates.Add(new Vector2Int(x, y));
        }
        foreach (Vector2Int coor in coordinates)
        {
            figureDatas.Add(new Pawn(coor.x, coor.y, "b_pawn", TypeFigure.Black));
        }
        deck.Add(new Pawn(0, 0, "b_pawn", TypeFigure.Black));
        deck.Add(new Pawn(0, 0, "b_pawn", TypeFigure.Black));

        Rival = new Rival(figureDatas)
        {
            Deck = deck
        };
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Earthquake"), Main.Instance.Canvas.gameObject.transform).GetComponent<Event>();
    }
}
public class Level2 : Level
{
    public override void Init()
    {
        List<CardData> deck = new()
        {
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black)
        };
        Rival = new Rival(new List<CardData>()
        {
            new Pawn(2,1, "b_pawn", TypeFigure.Black),
            new Pawn(3,2, "b_pawn", TypeFigure.Black),
            new Pawn(4,2, "b_pawn", TypeFigure.Black),
            new Pawn(5,1, "b_pawn", TypeFigure.Black),
            new Knight(3,1, "b_knight", TypeFigure.Black),
            new Knight(4,1, "b_knight", TypeFigure.Black),

        }) 
        {
            Deck = deck 
        };
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Earthquake"), Main.Instance.Canvas.gameObject.transform).GetComponent<Event>();
       

    }
}
public class Level3: Level
{
    public override void Init()
    {
        int randomIvent = Random.Range(0, 2);
        List<CardData> deck = new();
        List<CardData> figures = new();
        switch (randomIvent)
        {
            case 0:
                figures.Add(new Hog(1, 1, "b_hog", TypeFigure.Black));
                figures.Add(new Hog(3, 2, "b_hog", TypeFigure.Black));
                figures.Add(new Hog(4, 2, "b_hog", TypeFigure.Black));
                figures.Add(new Hog(6, 1, "b_hog", TypeFigure.Black));
                deck.Add(new Hog(0, 0, "b_hog", TypeFigure.Black));
                deck.Add(new Hog(0, 0, "b_hog", TypeFigure.Black));
                deck.Add(new Hog(0, 0, "b_hog", TypeFigure.Black));
                deck.Add(new Hog(0, 0, "b_hog", TypeFigure.Black));
                break;
            case 1:
                figures.Add(new Wolf(1, 1, "b_wolf", TypeFigure.Black));
                figures.Add(new Wolf(3, 2, "b_wolf", TypeFigure.Black));
                figures.Add(new Wolf(4, 2, "b_wolf", TypeFigure.Black));
                figures.Add(new Wolf(6, 1, "b_wolf", TypeFigure.Black));
                deck.Add(new Wolf(0, 0, "b_wolf", TypeFigure.Black));
                deck.Add(new Wolf(0, 0, "b_wolf", TypeFigure.Black));
                deck.Add(new Wolf(0, 0, "b_wolf", TypeFigure.Black));
                deck.Add(new Wolf(0, 0, "b_wolf", TypeFigure.Black));
                break;
        }
        Rival = new Rival(figures)
        {
            Deck = deck
        };
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Earthquake"), Main.Instance.Canvas.gameObject.transform).GetComponent<Event>();
    }
   
}
public class Level4 : Level
{
    public override void Init()
    {

        List<CardData> deck = new()
        {
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Bishop(0, 0, "b_bishop", TypeFigure.Black),
            new Bishop(0, 0, "b_bishop", TypeFigure.Black)
        };

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
        })
        {
            Deck = deck
        };
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Winter"), Main.Instance.Canvas.gameObject.transform).GetComponent<Event>();
    }
}
public class Level5 : Level
{
    public override void Init()
    {
        List<CardData> deck = new()
        {
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Rook(0, 0, "b_rook", TypeFigure.Black),
            new Rook(0, 0, "b_rook", TypeFigure.Black)
        };
        Rival = new Rival(new List<CardData>()
        {
            new Bishop(1,0, "b_bishop", TypeFigure.Black),
            new Bishop(6,0, "b_bishop", TypeFigure.Black),
            new Knight(2,0, "b_knight", TypeFigure.Black),
            new Knight(5,0, "b_knight", TypeFigure.Black),
            new Pawn(0,1, "b_pawn", TypeFigure.Black),
            new Pawn(1,1, "b_pawn", TypeFigure.Black),
            new Pawn(2,1, "b_pawn", TypeFigure.Black),
            new Pawn(3,1, "b_pawn", TypeFigure.Black),
            new Pawn(4,1, "b_pawn", TypeFigure.Black),
            new Pawn(5,1, "b_pawn", TypeFigure.Black),
            new Pawn(6,1, "b_pawn", TypeFigure.Black),
            new Pawn(7,1, "b_pawn", TypeFigure.Black)
        })
        {
            Deck = deck
        };
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Winter"), Main.Instance.Canvas.gameObject.transform).GetComponent<Event>();
    }
}
public class Level6 : Level
{
    public override void Init()
    {
        List<CardData> deck = new()
        {
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Knight(0, 0, "b_knight", TypeFigure.Black),
            new Knight(0, 0, "b_knight", TypeFigure.Black),
            new Rook(0, 0, "b_rook", TypeFigure.Black),
            new Rook(0, 0, "b_rook", TypeFigure.Black)
        };
        Rival = new Rival(new List<CardData>()
        {
            new Rook(3,1, "b_rook", TypeFigure.Black),
            new Rook(4,1, "b_rook", TypeFigure.Black),
            new Knight(1, 1, "b_knight", TypeFigure.Black),
            new Knight(6, 1, "b_knight", TypeFigure.Black),
            new Bishop(0, 1, "b_bishop", TypeFigure.Black),
            new Bishop(7, 1, "b_bishop", TypeFigure.Black),
            new Pawn(0,2, "b_pawn", TypeFigure.Black),
            new Pawn(1,2, "b_pawn", TypeFigure.Black),
            new Pawn(2,2, "b_pawn", TypeFigure.Black),
            new Pawn(3,2, "b_pawn", TypeFigure.Black),
            new Pawn(4,2, "b_pawn", TypeFigure.Black),
            new Pawn(5,2, "b_pawn", TypeFigure.Black),
            new Pawn(6,2, "b_pawn", TypeFigure.Black),
            new Pawn(7,2, "b_pawn", TypeFigure.Black)
        })
        {
            Deck = deck
        };

        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Fog"), Main.Instance.GUI).GetComponent<Event>();
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

                for(int i = 0; i < 6; i++)
                    deck.Add(new Hog(0, 0, "b_hog", TypeFigure.Black));


                break;
            case 1:
                for (int i = 2; i < 6; i++)
                    figures.Add(new Wolf(i, 2, "b_wolf", TypeFigure.Black));

                for (int i = 0; i < 6; i++)
                    deck.Add(new Wolf(0, 0, "b_wolf", TypeFigure.Black));
               

                break;
        }
        Rival = new Rival(figures)
        {
            Deck = deck
        };
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Fog"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level8 : Level
{
    public override void Init()
    {
        List<CardData> deck = new()
        {
            new Knight(0, 0, "b_knight", TypeFigure.Black),
            new Knight(0, 0, "b_knight", TypeFigure.Black),
            new Knight(0, 0, "b_knight", TypeFigure.Black),
            new Rook(0, 0, "b_rook", TypeFigure.Black),
            new Rook(0, 0, "b_rook", TypeFigure.Black)
        };
        Rival = new Rival(new List<CardData>()
        {
            
            new Pawn(0,3, "b_pawn", TypeFigure.Black),
            new Pawn(1,3, "b_pawn", TypeFigure.Black),
            new Pawn(2,3, "b_pawn", TypeFigure.Black),
            new Pawn(3,3, "b_pawn", TypeFigure.Black),
            new Pawn(4,3, "b_pawn", TypeFigure.Black),
            new Pawn(5,3, "b_pawn", TypeFigure.Black),
            new Pawn(6,3, "b_pawn", TypeFigure.Black),
            new Pawn(7,3, "b_pawn", TypeFigure.Black),
            new Bishop(0,2, "b_bishop", TypeFigure.Black),
            new Bishop(2,2, "b_bishop", TypeFigure.Black),
            new Bishop(5,2, "b_bishop", TypeFigure.Black),
            new Bishop(7,2, "b_bishop", TypeFigure.Black),
            new Rook(3,1, "b_rook", TypeFigure.Black),
            new Rook(4,1, "b_rook", TypeFigure.Black),
            new Knight(1,1, "b_knight", TypeFigure.Black),
            new Knight(6,1, "b_knight", TypeFigure.Black),
            new Pawn(0,0, "b_pawn", TypeFigure.Black),
            new Pawn(1,0, "b_pawn", TypeFigure.Black),
            new Pawn(2,0, "b_pawn", TypeFigure.Black),
            new Pawn(3,0, "b_pawn", TypeFigure.Black),
            new Pawn(4,0, "b_pawn", TypeFigure.Black),
            new Pawn(5,0, "b_pawn", TypeFigure.Black),
            new Pawn(6,0, "b_pawn", TypeFigure.Black),
            new Pawn(7,0, "b_pawn", TypeFigure.Black)
        })
        {
            Deck = deck
        };
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Fog"), Main.Instance.GUI.parent).GetComponent<Event>();
    }
}
public class Level9 : Level
{
    public override void Init()
    {
        List<CardData> deck = new()
        {
            new Knight(0, 0, "b_knight", TypeFigure.Black),
            new Knight(0, 0, "b_knight", TypeFigure.Black),
            new Rook(0, 0, "b_rook", TypeFigure.Black),
            new Rook(0, 0, "b_rook", TypeFigure.Black),
            new Bishop(0, 0, "b_bishop", TypeFigure.Black),
            new Bishop(0, 0, "b_bishop", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
            new Pawn(0, 0, "b_pawn", TypeFigure.Black),
        };
        Rival = new Rival(new List<CardData>()
        {
            new Pawn(0,2, "b_pawn", TypeFigure.Black),
            new Pawn(1,2, "b_pawn", TypeFigure.Black),
            new Pawn(2,2, "b_pawn", TypeFigure.Black),
            new Pawn(3,2, "b_pawn", TypeFigure.Black),
            new Pawn(4,2, "b_pawn", TypeFigure.Black),
            new Pawn(5,2, "b_pawn", TypeFigure.Black),
            new Pawn(6,2, "b_pawn", TypeFigure.Black),
            new Pawn(7,2, "b_pawn", TypeFigure.Black),
            new Rook(0,1, "b_rook", TypeFigure.Black),
            new Rook(7,1, "b_rook", TypeFigure.Black),
            new Knight(1,1, "b_knight", TypeFigure.Black),
            new Bishop(2,1, "b_bishop", TypeFigure.Black),
            new Bishop(5,1, "b_bishop", TypeFigure.Black),
            new Pawn(3,1, "b_pawn", TypeFigure.Black),
            new Pawn(4,1, "b_pawn", TypeFigure.Black),
            
        })
        {
            Deck = deck
        };
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Treason"), Main.Instance.GUI.parent).GetComponent<Event>();
    }
}
