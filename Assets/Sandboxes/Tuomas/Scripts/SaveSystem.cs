using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem

{
    public static void SavePlayer(PlayerAttributes player) //tallentaa pelaajan statsit binäärimuodossa tiedostoon
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile.txt";
        FileStream stream = new FileStream(path, FileMode.Create);


        PlayerData data = new PlayerData();

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("game saved");

    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData savedData = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            Debug.Log("Load succesful");
            return savedData;
        }
        else
        {
            Debug.Log("savefile not found in location: " + Application.persistentDataPath + "/savefile.txt");
            return null;
        }
    }
}
