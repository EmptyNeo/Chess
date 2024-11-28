using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

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
        Rival = new Rival(new ()
        {
            new Pawn(2,2, "b_pawn", TypeFigure.Black),
            new Pawn(3,2, "b_pawn", TypeFigure.Black),
            new Pawn(4,2, "b_pawn", TypeFigure.Black),
            new Pawn(5,2, "b_pawn", TypeFigure.Black)
        })
        {
            Figure = new()
            {
                new Pawn(0,0, "b_pawn", TypeFigure.Black),
                new Pawn(0,0, "b_pawn", TypeFigure.Black),
                new Pawn(0,0, "b_pawn", TypeFigure.Black),
                new Pawn(0,0, "b_pawn", TypeFigure.Black)
            }
        };
    }
}
public class Level1 : Level
{
    public override void Init()
    {
        Rival = new Rival(new List<FigureData>()
        {
            new Pawn(2,2, "b_pawn", TypeFigure.Black),
            new Pawn(3,2, "b_pawn", TypeFigure.Black),
            new Pawn(4,2, "b_pawn", TypeFigure.Black),
            new Pawn(5,2, "b_pawn", TypeFigure.Black)
        });

        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Earthquake"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level2: Level
{
    public override void Init()
    {
        Rival = new Rival(new List<FigureData>()
        {
            new Pawn(2,1, "b_pawn", TypeFigure.Black),
            new Pawn(3,1, "b_pawn", TypeFigure.Black),
            new Pawn(4,1, "b_pawn", TypeFigure.Black),
            new Pawn(5,1, "b_pawn", TypeFigure.Black)
        });
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Earthquake"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level3 : Level
{
    public override void Init()
    {
        Rival = new Rival(new List<FigureData>()
        {
            new Pawn(2,1, "b_pawn", TypeFigure.Black),
            new Pawn(3,1, "b_pawn", TypeFigure.Black),
            new Pawn(4,1, "b_pawn", TypeFigure.Black),
            new Pawn(5,1, "b_pawn", TypeFigure.Black)
        });

        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Winter"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level4 : Level
{
    public override void Init()
    {
        Rival = new Rival(new List<FigureData>()
        {
            new Pawn(2,1, "b_pawn", TypeFigure.Black),
            new Pawn(3,1, "b_pawn", TypeFigure.Black),
            new Pawn(4,1, "b_pawn", TypeFigure.Black),
            new Pawn(5,1, "b_pawn", TypeFigure.Black)
        });

        Main.Instance.Ivent =  Object.Instantiate(PrefabUtil.Load("Winter"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level5 : Level
{
    public override void Init()
    {
        Rival = new Rival(new List<FigureData>()
        {
            new Pawn(2,1, "b_pawn", TypeFigure.Black),
            new Pawn(3,1, "b_pawn", TypeFigure.Black),
            new Pawn(4,1, "b_pawn", TypeFigure.Black),
            new Pawn(5,1, "b_pawn", TypeFigure.Black)
        });

        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Smoke"), Main.Instance.GUI).GetComponent<Event>();
    }
}
public class Level6 : Level
{
    public override void Init()
    {
        Rival = new Rival(new List<FigureData>()
        {
            new Pawn(2,1, "b_pawn", TypeFigure.Black),
            new Pawn(3,1, "b_pawn", TypeFigure.Black),
            new Pawn(4,1, "b_pawn", TypeFigure.Black),
            new Pawn(5,1, "b_pawn", TypeFigure.Black)
        });
        Main.Instance.Ivent = Object.Instantiate(PrefabUtil.Load("Treason"), Main.Instance.GUI.parent).GetComponent<Event>();
    }
}
