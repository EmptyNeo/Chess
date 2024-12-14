using UnityEngine;

public static class SpriteUtil 
{
    public static Sprite Load(string imageName, string spriteName)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Figure/" + imageName);
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name == spriteName)
                return sprites[i];
        }
        return null;
    }
}
