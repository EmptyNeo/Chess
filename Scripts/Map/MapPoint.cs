using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public int Index;
    public int IndexLevel;
    private void OnDrawGizmos()
    {
        DrawText.Set(IndexLevel.ToString(), transform.position + Vector3.up, Color.white);
    }
}
