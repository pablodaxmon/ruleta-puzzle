using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
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

    public GameNiveles gameNiveles;
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
    private int nivelactual;
    private GameManager gameManager;
    private Desordenador desordenador;

    private UIController uIController;

    private UIStatusGameController statusGameController;

    private float movimientos;
    private float timeStartLevel;
    private float timeFinishLevel;
    public float movimientosPerfectos { get; set; }
    private float eficiencia;

    private MainGame mainGame;

    [Header("Generacion de niveles")]
    public GameObject sombraObject;
    public GameObject brillito;

    public List<Color> colores;
    public GameObject nivelMaster;
    public Sprite[] sombras;
    public Sprite[] contorno;
    public Sprite[] bordes;

    public GameObject guias;
    public bool[] isCenterLevels;
    public bool[] isTopLevels;
    public Vector2[][] posicionesEsferas = new Vector2[14][]
    {
        new Vector2[]{//forma 1
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0)}, 
        new Vector2[]{// forma 2 
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(6,0)}, 
        new Vector2[]{// forma 3 
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(6,0),
            new Vector2(-6,0)}, 
        new Vector2[]{// forma 4
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(0,3),
            new Vector2(0,-3)},
        new Vector2[]{ // forma 5
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(0,3),
            new Vector2(0,-3),
            new Vector2(6,0) },
        new Vector2[]{ // forma 6
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(-6,0),
            new Vector2(6,0),
            new Vector2(-3,3),
            new Vector2(-3,-3)},
        new Vector2[]{ // forma 7
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(6,0),
            new Vector2(0,3),
            new Vector2(0,-3),
            new Vector2(3,3),
            new Vector2(3,-3) },
        new Vector2[]{ // forma 8
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(0,3),
            new Vector2(0,-3),
            new Vector2(-3,3),
            new Vector2(3,-3) },
        new Vector2[]{ // forma 9
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(6,0),
            new Vector2(-3,3),
            new Vector2(0,3),
            new Vector2(3,3),
            new Vector2(6,3) },
        new Vector2[]{ // forma 10
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(-6,0),
            new Vector2(6,0),

            new Vector2(0,3),
            new Vector2(-3,3),
            new Vector2(3,3),
            new Vector2(-6,3),
            new Vector2(6,3)},
        new Vector2[]{ // forma 11
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(0,3),
            new Vector2(-3,3),
            new Vector2(3,3),
            new Vector2(0,-3),
            new Vector2(-3,-3),
            new Vector2(3,-3) },
        new Vector2[]{ // forma 12
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(0,3),
            new Vector2(-3,3),
            new Vector2(3,3),
            new Vector2(0,-3),
            new Vector2(-3,-3),
            new Vector2(3,-3),
            new Vector2(6,0),
            new Vector2(-6,0)},
        new Vector2[]{ // forma 13 
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(0,3),
            new Vector2(-3,3),
            new Vector2(3,3),
            new Vector2(0,-3),
            new Vector2(-3,-3),
            new Vector2(3,-3),
            new Vector2(6,3),
            new Vector2(6,0),
            new Vector2(6,-3) },
        new Vector2[]{ // forma 14
            new Vector2(0,0),
            new Vector2(-3,0),
            new Vector2(3,0),
            new Vector2(0,3),
            new Vector2(-3,3),
            new Vector2(3,3),
            new Vector2(0,-3),
            new Vector2(-3,-3),
            new Vector2(3,-3),
            new Vector2(6,3),
            new Vector2(6,0),
            new Vector2(6,-3),
            new Vector2(-6,3),
            new Vector2(-6,0),
            new Vector2(-6,-3)},
        
    };
    public void Start()
    {
        gameNiveles = GetComponent<GameNiveles>();
        nivelMaster.SetActive(true);
        uIController = GameObject.Find("UIController").GetComponent<UIController>();

        mainGame = GetComponent<MainGame>();
        gameManager = GetComponent<GameManager>();
        desordenador = GetComponent<Desordenador>();
        statusGameController = GameObject.Find("UIController").GetComponent<UIStatusGameController>();
        
        puntaje = 0;
        foreach (GameObject centro in GameObject.FindGameObjectsWithTag("Centro"))
        {

            centro.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    

    //primero se crea el nivel luego se llama a PLAY
    public void createGame(int nivel)
    {
        //GameObject.Instantiate(nivel, Vector3.zero, Quaternion.identity);
        showSombra();
        InstantiateLevel(nivel);
        centros = getCentros();
        foreach(GameObject cent in centros)
        {
            cent.GetComponent<CentroControlle>().SetStart();
        }
        rotables = getRotables();
        movimientos = 0;
        timeStartLevel = Time.time;
        movimientosPerfectos = 0;
    }
    public void disableCenters()
    {
        foreach (GameObject centro in GameObject.FindGameObjectsWithTag("Centro"))
        {


            centro.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    public void showSombra()
    {
        sombraObject.SetActive(true);
    }

    public void hideSombra()
    {
        sombraObject.SetActive(false);
    }
    public void enableCenters(){
        foreach (GameObject centro in GameObject.FindGameObjectsWithTag("Centro"))
        {


            centro.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

    
    public void InstantiateLevel(int nivel)
    {
        GameObject nivelelement = GameObject.Instantiate(nivelMaster,Vector3.zero, Quaternion.identity);
        
        int tipo = gameNiveles.niveles[nivel].type;
        string colores = gameNiveles.niveles[nivel].colors;
        nivelactual = nivel;
        sombraObject.GetComponent<SpriteRenderer>().sprite = sombras[tipo];

        List<GameObject> objetosEnJuego = new List<GameObject>();
        foreach (Transform child in nivelelement.transform)
        {
            objetosEnJuego.Add(child.gameObject);
        }
        float multCenter = 0;
        float multTop = 0;

        if (isCenterLevels[tipo] == false)
        {

            multCenter = -1.5f;

            guias.transform.position = new Vector3(multCenter,0,0);
        }
        if(isTopLevels[tipo] == true)
        {
            multTop = -1.5f;
        }

        guias.transform.position = new Vector3(multCenter, multTop, 0);
        int countJ = nivelelement.transform.childCount - 1;
        /*for (int i = 0; i < posicionesEsferas[tipo].Length;i++)
        {
            GameObject.Instantiate(brillito.transform, posicionesEsferas[tipo][i], Quaternion.identity);
            for (int j = countJ; j >= 0; j--)
            {
                if(Vector2.Distance(posicionesEsferas[tipo][i],nivelelement.transform.GetChild(j).position) < 1.76f)
                {
                    if(nivelelement.transform.GetChild(j).tag == "Centro")
                    {
                        nivelelement.transform.GetChild(j).GetChild(0).GetComponent<SpriteRenderer>().color = getColorById(gameNiveles.niveles[nivel].colors[i].ToString()); 
                        nivelelement.transform.GetChild(j).GetChild(1).GetComponent<SpriteRenderer>().color = getColorById(gameNiveles.niveles[nivel].colors[i].ToString());
                    } else
                    {
                        nivelelement.transform.GetChild(j).GetComponent<SpriteRenderer>().color = getColorById(gameNiveles.niveles[nivel].colors[i].ToString());
                    }

                    objetosEnJuego.Add(nivelelement.transform.GetChild(j).gameObject); 
                    nivelelement.transform.GetChild(j).SetParent(null);
                    count
                    
                }
                Debug.Log(nivelelement.transform.GetChild(j).name);
            }

            
        }*/

        
        for(int i = countJ; i >= 0; i--)
        {
            bool match = false;
            for (int j = 0; j < posicionesEsferas[tipo].Length; j++)
            {
                if (Vector2.Distance(posicionesEsferas[tipo][j], nivelelement.transform.GetChild(i).position) < 1.76f)
                {
                    match = true;
                    if (nivelelement.transform.GetChild(i).tag == "Centro")
                    {
                        nivelelement.transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().color = getColorById(gameNiveles.niveles[nivel].colors[j].ToString());
                        nivelelement.transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().color = getColorById(gameNiveles.niveles[nivel].colors[j].ToString());
                    }
                    else
                    {
                        nivelelement.transform.GetChild(i).GetComponent<SpriteRenderer>().color = getColorById(gameNiveles.niveles[nivel].colors[j].ToString());
                        
                    }
                    
                }
                
            }

            if (match == false)
            {
                GameObject.DestroyImmediate(nivelelement.transform.GetChild(i).gameObject);
                

            } else
            {
                nivelelement.transform.GetChild(i).position += new Vector3(multCenter,multTop,0);
            }
        }


        enableCenters();



    }
    
    public void restartLevel()
    {
        Debug.Log("Reiniciando nivel");
        isPlaying = false;
        clear();

        Debug.Log("nivel limpiado");
        createGame(nivelactual);

        Debug.Log("nuevo nivel creado");
        Play();
        Debug.Log("nuevo nivel iniciado");
    }

    private Color getColorById(string id)
    {
        switch (id)
        {
            case "1":
                return colores[0];
            case "2":
                return colores[1];
            case "3":
                return colores[2];
            case "4":
                return colores[3];
            case "5":
                return colores[4];
            case "6":
                return colores[5];
            case "7":
                return colores[6];
            case "8":
                return colores[7];
            case "9":
                return colores[8];
            case "A":
                return colores[9];
            case "B":
                return colores[10];
            case "C":
                return colores[11];
            case "D":
                return colores[12];
            case "E":
                return colores[13];
            case "F":
                return colores[14];
            case "G":
                return colores[15];
            case "H":
                return colores[16];
            case "I":
                return colores[17];
            case "J":
                return colores[18];
            case "K":
                return colores[19];
            case "L":
                return colores[20];
            case "M":
                return colores[21];
            case "N":
                return colores[22];
            case "O":
                return colores[23];
            case "P":
                return colores[24];
            case "Q":
                return colores[25];
            case "R":
                return colores[26];
            case "S":
                return colores[27];
            case "T":
                return colores[28];
            case "U":
                return colores[29];
            case "V":
                return colores[30];
            case "W":
                return colores[31];
            case "X":
                return colores[32];
            case "Y":
                return colores[33];
            case "Z":
                return colores[34];
            default:
                return Color.black;
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
        if (!isPlaying)
        {
            movimientosPerfectos++;
        }

        rotables = getRotables();
        puntaje = gameManager.calculatePoints(centros, rotables);
        statusGameController.updatePoints(puntaje);
        
        verificarLevelComplete(puntaje);
        if (isPlaying)
        {
            mainGame.playSoundCircleComplete();
        }
        

    }

    private void listenerClickCentro(object sender, GameObject gameObjectSender)
    {
        positionCentro = gameObjectSender.transform.position;
        Vector3 correctPosition = new Vector3(gameObjectSender.transform.position.x, gameObjectSender.transform.position.y, 0);
        GameObject.Instantiate(circleClickEffect, correctPosition, Quaternion.identity);
        movimientos++;
        mainGame.playSoundClick();
    }
    private void verificarLevelComplete(int valor){
        if(puntaje == puntajeMaximo && isPlaying)
        {
            levelComplete();
        }
    }
    public void levelComplete()
    {

        finishedLevel();
        mainGame.playSoundWin();
        mainGame.stopSong();
        timeFinishLevel = Time.time;
        float duracion = timeFinishLevel - timeStartLevel;
        eficiencia = ((movimientosPerfectos + (movimientosPerfectos/2)) /(movimientos + +(movimientosPerfectos / 2))) * 100;
        eficiencia = eficiencia > 100 ? 100 : eficiencia;
        uIController.levelComplete(movimientos, duracion, eficiencia);
        mainGame.saveLevelComplete((int)movimientos,(timeFinishLevel-timeStartLevel),eficiencia);

    }
    public void finishedLevel()
    {
        mainGame.setUIcontrolerUpdateLevels();
        CentroControlle.rotando = true;
        isPlaying = false;
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
        List<GameObject> nivelElements = new List<GameObject>(GameObject.FindGameObjectsWithTag("NivelElement"));
        List<GameObject> rotables = new List<GameObject>(GameObject.FindGameObjectsWithTag("Rotable"));
        List<GameObject> centros = new List<GameObject>(GameObject.FindGameObjectsWithTag("Centro"));

        List<GameObject> esquinas = new List<GameObject>(GameObject.FindGameObjectsWithTag("Esquina")); 
        
        foreach (GameObject esquina in esquinas)
        {
            GameObject.DestroyImmediate(esquina);
        }
        foreach (GameObject rotable in rotables)
        {
            GameObject.DestroyImmediate(rotable);
        }
        foreach (GameObject centro in centros)
        {
            GameObject.DestroyImmediate(centro);
        }
        foreach (GameObject element in nivelElements)
        {
            GameObject.DestroyImmediate(element);
        }
    }


    
}



