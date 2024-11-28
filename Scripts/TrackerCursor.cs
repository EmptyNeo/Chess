using UnityEngine;

public class TrackerCursor : MonoBehaviour
{
    public SpriteRenderer Cursor;
    public Sprite FingerUp;
    public Sprite FingerDown;
    private void Start()
    {
        Cursor.gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void Update()
    {
        UnityEngine.Cursor.visible = false;
        Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Cursor.gameObject.transform.position = position;
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.sprite = FingerDown;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Cursor.sprite = FingerUp;
        }
    }
}
