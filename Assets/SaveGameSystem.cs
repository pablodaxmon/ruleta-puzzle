using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int nivelAlcanzado;

    public List<int> movimientos;
    public List<float> tiempo;
    public List<float> eficiencia;
}

public class SaveGameSystem
{

    //leemos el archivo gamesave.save y devolvemos el parametro 
    public Save loadGame()
    {
        Save save;
        // si existe el archivo entonces lo abrimos y guardamos sus datos en SAVE
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            save = (Save)bf.Deserialize(file);
            file.Close();
            return save;
        }
        else
        {
            //si no existe creamos una partida
            Debug.Log("Partida nueva");
            return createGame();
        }
    }

    //Pasamos el parametro nivel al archivo gamesave.save
    public void saveGame(Save saveG)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            File.Delete(Application.persistentDataPath + "/gamesave.save");
        }
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save"); 
        bf.Serialize(file, saveG);
        file.Close();
    }

    //Guardamos el nivel inicial que es 0 en un archivo llamado gamesave.save
    private Save createGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        Save save = new Save();
        save.nivelAlcanzado = 0;
        save.tiempo = new List<float>();
        
        save.movimientos = new List<int>();

        save.eficiencia = new List<float>();

        bf.Serialize(file, save);
        file.Close();

        return save;
    }
}
