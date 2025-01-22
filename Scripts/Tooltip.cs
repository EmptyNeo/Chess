using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public RectTransform RectTransform;
    public TMP_Text Description;
    public TMP_Text Name;
    public GameObject View;

    public void SetDescription(string name,string description)
    {
        Name.text = name;
        Description.text = description;
        RectTransform.sizeDelta = new Vector2(Name.preferredWidth + Description.preferredWidth + 10, Name.preferredHeight + Description.preferredHeight + 20);
        
    }
}