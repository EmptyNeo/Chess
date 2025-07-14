using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public RectTransform RectTransform;
    public TMP_Text Description;
    public TMP_Text Name;
    public RectTransform View;
    private void Update()
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            Main.Instance.Canvas.transform as RectTransform,
            Input.mousePosition,
            Camera.main,
            out Vector2 localPosition))
        {
            View.anchoredPosition = localPosition;
        }
    }
    public void SetDescription(string name, string description)
    {
        Name.text = name;
        Description.text = description;
        RectTransform.sizeDelta = new Vector2(Name.preferredWidth + Description.preferredWidth + 10, Name.preferredHeight + Description.preferredHeight + 20);

    }
}