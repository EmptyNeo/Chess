using UnityEngine;

public class SoundFX
{
    public AudioClip Clip { get; protected set; }
}
public class SoundTakeCard : SoundFX
{
    public SoundTakeCard():base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/take_card");
    }

}
public class SoundExposeFigure : SoundFX
{
    public SoundExposeFigure() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/expose_figure");
    }

}
public class SoundExposeCard : SoundFX
{
    public SoundExposeCard() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/expose_card");
    }

}
public class SoundExposeBarrel : SoundFX
{
    public SoundExposeBarrel() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/expose_barrel");
    }

}


public class SoundFreezing : SoundFX
{
    public SoundFreezing() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/freezing");
    }
}
public class SoundEarthquake : SoundFX
{
    public SoundEarthquake() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/earthquake");
    }
}
public class SoundShield : SoundFX
{
    public SoundShield() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/shield");
    }
}
public class SoundDefrosting : SoundFX
{
    public SoundDefrosting() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/defrosting");
    }
}
public class SoundThunder : SoundFX
{
    public SoundThunder() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/thunder");
    }
}
public class SoundGiveCard : SoundFX
{
    public SoundGiveCard() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/give_card");
    }
}
public class SoundWin : SoundFX
{
    public SoundWin() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/win");
    }
}
public class SoundLose : SoundFX
{
    public SoundLose() : base()
    {
        Clip = Resources.Load<AudioClip>("Sounds/lose");
    }
}
