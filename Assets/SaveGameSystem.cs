using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveGameSystem
{

    //leemos el archivo gamesave.save y devolvemos el parametro 
    public int loadGame()
    {
        int save;
        // si existe el archivo entonces lo abrimos y guardamos sus datos en SAVE
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            save = (int)bf.Deserialize(file);
            file.Close();
            return save;
        }
        else
        {
            //si no existe creamos una partida
            Debug.Log("Partida nueva");
            createGame();
            return 0;
        }
    }

    //Pasamos el parametro nivel al archivo gamesave.save
    public void saveGame(int nivel)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            File.Delete(Application.persistentDataPath + "/gamesave.save");
        }
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save"); 
        bf.Serialize(file, nivel);
    }

    //Guardamos el nivel inicial que es 0 en un archivo llamado gamesave.save
    private void createGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        int nivelInicial = 0;
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, nivelInicial);
        file.Close();
    }
}
