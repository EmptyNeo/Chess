using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class BinarySavingSystem
{
    public static void SaveDeck(Deck deck)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/deck.a";
        FileStream stream = new(path, FileMode.Create);
        DataDeck data = new(deck);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static DataDeck LoadDeck()
    {
        string path = Application.persistentDataPath + "/deck.a";
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);
            DataDeck data = formatter.Deserialize(stream) as DataDeck;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static void DeleteDeck()
    {
        string path = Application.persistentDataPath + "/deck.a";
        Debug.Log(path);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
