using System;
using System.Collections.Generic;
[Serializable]
public class Level
{
    public Rival Rival;
    public virtual void Init()
    {

    }
}
public class Level1 : Level
{
    public override void Init()
    {
        Rival = new Rival(new List<FigureData>()
        {
            new Pawn(2,2, "b_pawn", ColorFigure.Black),
            new Pawn(3,2, "b_pawn", ColorFigure.Black),
            new Pawn(4,2, "b_pawn", ColorFigure.Black),
            new Pawn(5,2, "b_pawn", ColorFigure.Black)
        });
    }
}
public class Level2: Level
{
    public override void Init()
    {
        Rival = new Rival(new List<FigureData>()
        {
            new Pawn(2,1, "b_pawn", ColorFigure.Black),
            new Pawn(3,1, "b_pawn", ColorFigure.Black),
            new Pawn(4,1, "b_pawn", ColorFigure.Black),
            new Pawn(5,1, "b_pawn", ColorFigure.Black)
        });
    }
}