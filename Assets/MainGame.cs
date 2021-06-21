using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contiene los objetos UIController, la partida guardada y el juego principal.
public class MainGame : MonoBehaviour
{
    // Lista completa de los niveles
    public GameObject[] niveles;

    // Nivel mas alto alcanzado.
    private int saveGame = 0;

    // Crea, abre y guarda partidas.
    private SaveGameSystem saveGameSystem = new SaveGameSystem();

    

    // Controla toda la UI
    private UIController uicontroller;

    // Level actual index 

    // Level actual index
    public int nivelActual { get; set; }

    private Game game;

    // Inicial el juego
    private void Start()
    {
        game = GetComponent<Game>();

        uicontroller = GameObject.Find("UIController").GetComponent<UIController>();

        // Se carga el nivel avanzado
        readSaveGame();

        // Se le avisa al uicontroller que nivel es el mas avanzado para que actualice sus botones
        uicontroller.updateLevels(niveles, saveGame);

        // Se añade un listener al evento ClickBoton
        uicontroller.loadLevel += loadLevel;

        // Se añade un listener al evento levelComplete
        game.levelCompleteEvent += levelCompleteNotify;
    }

    public float getIndiceDeDesorden()
    {
        return niveles[nivelActual].GetComponent<Nivel>().dificultad;
    }

    // Inicial el nivel
    private void loadLevel(object sender, int nivel)
    {
        nivelActual = nivel;

        uicontroller.setTextReleaseLevel(
            "Titulo nivel " + (nivel + 1).ToString(),
            "Movimientos " + (nivel*5).ToString(),
            "tiempo no estimado!");
    }


    // Proporciona el indice el nuevo nivel y guarda el progreso.
    private void levelCompleteNotify()
    {
        nivelActual++;

        if (nivelActual > saveGame)
        {
            saveGame = nivelActual;
            saveGameSystem.saveGame(saveGame);
        }
    }

    public void setNewGame()
    {
        uicontroller.startNivel();

        game.clear();

        // Se copia una instancia del nivel actual
        game.createGame(niveles[nivelActual]);

        // Se inicia el nivel
        game.Play();
    }

    //Una vez notificado el nivel completo y actualizado el conteo de niveles, 
    //esta funciona puede ser llamada a travez del boton SiguienteNivel
    private void nextLevel()
    {
        setNewGame();
    }


    // Lee el nivel guardado en memoria.
    private void readSaveGame()
    {
        saveGame = saveGameSystem.loadGame();
    }

}
