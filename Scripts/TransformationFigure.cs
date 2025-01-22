using System.Collections.Generic;
using UnityEngine;

public class TransformationFigure : MonoBehaviour
{
    public GameObject List;
    public List<FigureData> FiguresDataWhite = new();
    public List<FigureData> FiguresDataBlack = new();
    private Slot _newSlot;
    private Slot _oldSlot;
    public void Init()
    {
        FiguresDataWhite.Add((FigureData)Main.Instance.Factory.GetFigure<Knight>(TypeFigure.White));
        FiguresDataWhite.Add((FigureData)Main.Instance.Factory.GetFigure<Rook>(TypeFigure.White));
        FiguresDataWhite.Add((FigureData)Main.Instance.Factory.GetFigure<Bishop>(TypeFigure.White));
        FiguresDataBlack.Add((FigureData)Main.Instance.Factory.GetFigure<Knight>(TypeFigure.Black));
        FiguresDataBlack.Add((FigureData)Main.Instance.Factory.GetFigure<Rook>(TypeFigure.Black));
        FiguresDataBlack.Add((FigureData)Main.Instance.Factory.GetFigure<Bishop>(TypeFigure.Black));
    }
    public void Init(Slot oldSlot, Slot newSlot)
    {
       
        _oldSlot = oldSlot;
        _newSlot = newSlot;
        List.SetActive(true);
        transform.position = oldSlot.transform.position;

    }

    public void SetFigure(GameObject gameObject)
    {
        _oldSlot.Nullify();
        _newSlot.SetCard(FiguresDataWhite[gameObject.transform.GetSiblingIndex()]);
        List.SetActive(false);
        StartCoroutine(Main.Instance.Back());
    }
   
    public void BackFigure()
    {
        _oldSlot.SetCard(_newSlot.CardData);
        _newSlot.Nullify();
        List.SetActive(false);
    }
}
