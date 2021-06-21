using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
public delegate void levelCompleteevent();
public class Game : MonoBehaviour
{
    public GameObject circleTurn;
    public GameObject circleClickEffect;
    public GameObject sleeveComplete;
    public event levelCompleteevent levelCompleteEvent;

    private List<GameObject> circulosAyuda;
    private List<GameObject> centros;
    private List<GameObject> rotables;

    private List<Vector3> vectoresHojalesCompletos = new List<Vector3>();

    private Vector3 positionCentro;
    private int puntaje;
    public int Puntaje {
        get {
            return this.puntaje;
        }
        set {
            this.puntaje = value;
            
        } 
    }
    private int puntajeMaximo;
    public bool isPlaying = false;

    private GameManager gameManager;
    private Desordenador desordenador;

    private UIStatusGameController statusGameController;
    public void Start()
    {
        gameManager = GetComponent<GameManager>();
        desordenador = GetComponent<Desordenador>();
        statusGameController = GameObject.Find("UIController").GetComponent<UIStatusGameController>();
        puntaje = 0;
    }

    

    //primero se crea el nivel luego se llama a PLAY
    public void createGame(GameObject nivel)
    {
        GameObject.Instantiate(nivel, Vector3.zero, Quaternion.identity);
        centros = getCentros();
        rotables = getRotables();
        crearCirculosRotables();
    }

    private void crearCirculosRotables()
    {
        Vector3 position;
        circulosAyuda = new List<GameObject>();
        foreach (GameObject centro in centros)
        {
            position = new Vector3(centro.transform.position.x, centro.transform.position.y,-5);
            GameObject newCircle = GameObject.Instantiate(circleTurn,position, Quaternion.identity);
            newCircle.SetActive(false);
            circulosAyuda.Add(newCircle);
        }
    }

    public void mostrarOcultarCirculosRotables()
    {
        foreach (GameObject circulo in circulosAyuda)
        {
            if (circulo.activeInHierarchy)
            {
                circulo.SetActive(false);
            } else
            {

                circulo.SetActive(true);
            }
        }
    }
    public void Play()
    {
        addListenerCentrosCLicked();
        puntaje = gameManager.calculatePoints(centros, rotables);
        puntajeMaximo = puntaje;
        desordenador.startDesorden(centros);
        statusGameController.setMaxPoints(puntajeMaximo);
        statusGameController.updatePoints(puntaje);
    }
    private void addListenerCentrosCLicked()
    {
        foreach (GameObject centro in getCentros())
        {
            centro.GetComponent<CentroControlle>().giroCompleto += listenerGiroCompleto;
            centro.GetComponent<CentroControlle>().centroClickDown += listenerClickCentro;
        }
    }

    private void listenerGiroCompleto(object sender, GameObject gameObjectSender)
    {
        rotables = getRotables();
        puntaje = gameManager.calculatePoints(centros, rotables);
        statusGameController.updatePoints(puntaje);
        
        verificarLevelComplete(puntaje);
        

    }

    private void listenerClickCentro(object sender, GameObject gameObjectSender)
    {
        positionCentro = gameObjectSender.transform.position;
        Vector3 correctPosition = new Vector3(gameObjectSender.transform.position.x, gameObjectSender.transform.position.y, 0);
        GameObject.Instantiate(circleClickEffect, correctPosition, Quaternion.identity);
    }

    private void verificarLevelComplete(int valor){
        if(puntaje == puntajeMaximo)
        {
            levelComplete();
        }
    }
    public void levelComplete()
    {
        CentroControlle.rotando = true;

        statusGameController.levelComplete();

    }

    public List<GameObject> getCentros()
    {
        return new List<GameObject>(GameObject.FindGameObjectsWithTag("Centro"));
    }
    public List<GameObject> getRotables()
    {
        return new List<GameObject>(GameObject.FindGameObjectsWithTag("Rotable"));
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
        foreach (GameObject centro in centros)
        {
            GameObject.DestroyImmediate(centro);
        }
    }


    
}
