using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public int Index;
    public int IndexLevel;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        DrawText.Set(IndexLevel.ToString(), transform.position + Vector3.up, Color.white);
    }
#endif
}
