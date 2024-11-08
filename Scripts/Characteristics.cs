using TMPro;
using UnityEngine;

public class Characteristics : MonoBehaviour
{
    private int _mana;
    public int Mana 
    { 
        get => _mana;
        set
        {
            if (value > 0)
                _mana = value;
            else _mana = 0;
        } 
    }
    public int MaxMana;
    public TMP_Text Amount;
    public static Characteristics Instance { get; private set; }
    private void Start()
    {
        _mana = MaxMana;
        Instance = this;
        Amount.text = Mana + "/" + MaxMana;
    }
    public void TakeMana(int amount)
    {
        Mana -= amount;
        Amount.text = Mana + "/" + MaxMana;
    }
    public void AddMana(int amount)
    {
        Mana += amount;
        Amount.text = Mana + "/" + MaxMana;
    }
}
