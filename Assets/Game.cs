using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void levelCompleteevent();
public class Game : MonoBehaviour
{
    public event levelCompleteevent levelCompleteEvent;

    private GameManager gameManager;
    public void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    public void levelComplete()
    {

    }

    public void clear()
    {
        GameObject[] rotables = GameObject.FindGameObjectsWithTag("Rotable");
        GameObject[] centros = GameObject.FindGameObjectsWithTag("Centro");

        foreach (GameObject rotable in rotables)
        {
            GameObject.DestroyImmediate(rotable);
        }
        foreach (GameObject centro in centros)
        {
            GameObject.DestroyImmediate(centro);
        }
    }

    public void Play()
    {
        Debug.Log("Playing game!");
    }

    public void createGame(GameObject nivel)
    {
        GameObject.Instantiate(nivel,Vector3.zero,Quaternion.identity);
    }
}
