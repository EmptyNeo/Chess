using UnityEngine;

public class SoundFX
{
    public AudioClip Clip { get; protected set; }
}
public class SoundTakeCard : SoundFX
{
    public SoundTakeCard()
    {
        Clip = Resources.Load<AudioClip>("Sounds/take_card");
    }

}
public class SoundExposeFigure : SoundFX
{
    public SoundExposeFigure()
    {
        Clip = Resources.Load<AudioClip>("Sounds/expose_figure");
    }

}
public class SoundExposeCard : SoundFX
{
    public SoundExposeCard()
    {
        Clip = Resources.Load<AudioClip>("Sounds/expose_card");
    }

}
public class SoundExposeBarrel : SoundFX
{
    public SoundExposeBarrel()  
    {
        Clip = Resources.Load<AudioClip>("Sounds/expose_barrel");
    }

}
public class SoundFreezing : SoundFX
{
    public SoundFreezing()
    {
        Clip = Resources.Load<AudioClip>("Sounds/freezing");
    }
}
public class SoundEarthquake : SoundFX
{
    public SoundEarthquake()
    {
        Clip = Resources.Load<AudioClip>("Sounds/earthquake");
    }
}
public class SoundShield : SoundFX
{
    public SoundShield()
    {
        Clip = Resources.Load<AudioClip>("Sounds/shield");
    }
}
public class SoundDefrosting : SoundFX
{
    public SoundDefrosting()
    {
        Clip = Resources.Load<AudioClip>("Sounds/defrosting");
    }
}
public class SoundThunder : SoundFX
{
    public SoundThunder()
    {
        Clip = Resources.Load<AudioClip>("Sounds/thunder");
    }
}
public class SoundGiveCard : SoundFX
{
    public SoundGiveCard()
    {
        Clip = Resources.Load<AudioClip>("Sounds/give_card");
    }
}
public class SoundWin : SoundFX
{
    public SoundWin()
    {
        Clip = Resources.Load<AudioClip>("Sounds/win");
    }
}
public class SoundLose : SoundFX
{
    public SoundLose()
    {
        Clip = Resources.Load<AudioClip>("Sounds/lose");
    }
}
public class SoundClick : SoundFX
{
    public SoundClick()
    {
        Clip = Resources.Load<AudioClip>("Sounds/click");
    }
}