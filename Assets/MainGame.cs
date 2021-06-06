using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contiene los objetos UIController, la partida guardada y el juego principal.
public class MainGame : MonoBehaviour
{
    // Lista completa de los niveles
    private GameObject[] niveles;

    // Nivel mas alto alcanzado.
    private int saveGame = 0;

    // Crea, abre y guarda partidas.
    private SaveGameSystem saveGameSystem = new SaveGameSystem();

    // Nivel mostrado en pantalla
    private Game game = new Game();

    // Controla toda la UI
    private UIController uicontroller;


    // Inicial el juego
    private void Start()
    {
        // Se carga el nivel avanzado
        saveGame = saveGameSystem.loadGame();

        // Se le avisa al uicontroller que nivel es el mas avanzado para que actualice sus botones
        uicontroller.updateLevels(niveles, saveGame);

        // Se añade un listener al evento ClickBoton
        uicontroller.loadLevel += loadLevel;
    }


    // Inicial el nivel
    private void loadLevel(object sender, int nivel)
    {
        game.clear();

        // Se copia una instancia del nivel actual
        game.createGame(niveles[nivel]);

        // Se inicia el nivel
        game.Play();

    }

}
