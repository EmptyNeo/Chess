using UnityEngine;

public class PrefabUtil
{
    public static GameObject Load(string name)
    {
        return Resources.Load<GameObject>("Prefab/" + name);
    }
}