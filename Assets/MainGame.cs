using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Contiene los objetos UIController, la partida guardada y el juego principal.
public class MainGame : MonoBehaviour
{
    // Lista completa de los niveles
    public GameObject[] niveles;

    // Nivel mas alto alcanzado.
    private Save saveGame;

    // Crea, abre y guarda partidas.
    private SaveGameSystem saveGameSystem = new SaveGameSystem();

    private GameNiveles gameNiveles;


    // Controla toda la UI
    private UIController uicontroller;

    // Level actual index 

    // Level actual index
    public int nivelActual { get; set; }

    private Game game;

    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorialbuton;
    public GameObject panelTutorial;

    public TextMeshProUGUI tituloNivel;


    [Header("Clips de audio")]
    public AudioClip uiclip;
    public AudioClip effecto1;
    public AudioClip effectoClip;
    public AudioClip effectoWin;
    public AudioClip effectoCount;
    public AudioClip effectoLevelUp;
    public AudioClip effectoBell;


    [Header("Canciones")]
    public AudioClip songMenu;
    public AudioClip songInGame;

    [Header("Volumen sliders")]
    public Slider sliderEffects;
    public Slider sliderSongs;
    public Slider sliderEffectsMain;
    public Slider sliderSongsMain;
    private float volumenSongs;
    private float volumenEffects;

    [Header("Fuentes de sonido")]
    public AudioSource songs;
    public AudioSource audioEffects;
    private void Start()
    {
        songs.loop = true;
        gameNiveles = GetComponent<GameNiveles>();
        uicontroller = GameObject.Find("UIController").GetComponent<UIController>();
        int hasPlayed = PlayerPrefs.GetInt("HasPlayed");
        if (hasPlayed == 0)
        {

            PlayerPrefs.SetFloat("Songs",0.9f);

            PlayerPrefs.SetFloat("Effects", 0.9f);
            Debug.Log("Recibiendo volumen " + PlayerPrefs.GetFloat("Songs"));
            PlayerPrefs.SetInt("HasPlayed",1);
        } else
        {
            tutorial1.SetActive(false);
            tutorial2.SetActive(false);
            uicontroller.mostrarPaneles();
            panelTutorial.SetActive(false);

            sliderEffects.value = PlayerPrefs.GetFloat("Effects");
            sliderEffectsMain.value = PlayerPrefs.GetFloat("Effects");

            sliderSongs.value = PlayerPrefs.GetFloat("Songs");
            sliderSongsMain.value = PlayerPrefs.GetFloat("Songs");

            setVolumenEffects(PlayerPrefs.GetFloat("Effects"));
            setVolumenSong(PlayerPrefs.GetFloat("Songs"));

            sliderEffects.onValueChanged.AddListener(setVolumenEffects);
            sliderEffectsMain.onValueChanged.AddListener(setVolumenEffects);


            sliderSongs.onValueChanged.AddListener(setVolumenSong);
            sliderSongsMain.onValueChanged.AddListener(setVolumenSong);
            playSongMenu();
        }
        game = GetComponent<Game>();
        readSaveGame();
        setUIcontrolerUpdateLevels();
        uicontroller.loadLevel += loadLevel;
        uicontroller.finishStats += playSoundLevelUp;
        uicontroller.eventStats += playSoundCount;
        uicontroller.countStats += playSoundBell;
    }

    public void runTutorial()
    {
        game.clear();
        game.hideSombra();
        uicontroller.guias.SetActive(false);
        uicontroller.ocultarPaneles();
        tutorial1.SetActive(true);
        panelTutorial.SetActive(true);
    }
    public void playSoundUI()
    {
        audioEffects.PlayOneShot(uiclip, 1);

    }
    public void playSoundBell()
    {
        Debug.Log("BELL");
        audioEffects.PlayOneShot(effectoBell, 1);
        songs.Stop();
    }
    public void playSoundClick()
    {
        audioEffects.PlayOneShot(effecto1, 1);
    }
    public void playSoundWin()
    {
        audioEffects.PlayOneShot(effectoWin, 1);
    }
    public void playSoundLevelUp()
    {
        audioEffects.PlayOneShot(effectoLevelUp, 1);
        songs.Stop();
    }
    public void playSoundCircleComplete()
    {
        audioEffects.PlayOneShot(effectoClip, 1);
    }
    public void playSoundCount()
    {
        songs.clip = effectoCount;
        songs.Play();
    }
    public void playSongMenu()
    {
        StartCoroutine(fadeSongs(songMenu)); 
        
    }
    public void stopSong(){
        songs.Stop();
    }
    public void PlaySongInGame()
    {
        
        StartCoroutine(fadeSongs(songInGame));
        
    }
    public void setVolumenSong(float value)
    {
        Debug.Log("Dando volumen " + value);
        songs.volume = value;
        PlayerPrefs.SetFloat("Songs", value);

        Debug.Log("Ahora el songs guardo " + PlayerPrefs.GetFloat("Songs")); ;

    }
    public void setVolumenEffects(float value)
    {
        audioEffects.volume = value;
        PlayerPrefs.SetFloat("Effects", value);
    }
    public IEnumerator fadeSongs(AudioClip clip)
    {
        
        while (songs.volume > 0)
        {
            songs.volume -= Time.deltaTime * 1.5f;
            yield return null;
        }

        songs.volume = 0;
        songs.clip = clip;
        songs.Play();
        while (songs.volume < PlayerPrefs.GetFloat("Songs"))
        {
            songs.volume += Time.deltaTime * 1.5f;
            yield return null;
        }
        songs.volume = PlayerPrefs.GetFloat("Songs");
    }
    public void setUIcontrolerUpdateLevels()
    {
        uicontroller.updateLevels(niveles, saveGame.nivelAlcanzado);
    }
    public void showButonTutorial()
    {
        if(PlayerPrefs.GetInt("HasPlayed") == 0)
        {

            StartCoroutine(butonTutorial());
        }
    }
    public void tutorialNext()
    {
        
        panelTutorial.SetActive(true);
        tutorialbuton.SetActive(false);
        StartCoroutine(butonTutorialSinTutorial1());
        if (tutorial1.activeInHierarchy == true && tutorial2.activeInHierarchy == false)
        {
            tutorial2.SetActive(true);
            tutorial1.SetActive(false);
        } else if(tutorial1.activeInHierarchy == false && tutorial2.activeInHierarchy == true)
        {
            tutorial1.SetActive(false);
            tutorial2.SetActive(false);
            uicontroller.mostrarPaneles();
            panelTutorial.SetActive(false);
            playSongMenu();
        }
    }
    private IEnumerator butonTutorial()
    {

        tutorial1.SetActive(true);
        yield return new WaitForSeconds(7);
        tutorialbuton.SetActive(true);
    }
    private IEnumerator butonTutorialSinTutorial1()
    {

        yield return new WaitForSeconds(7);
        tutorialbuton.SetActive(true);
    }
    
    private void loadLevel(object sender, int nivel)
    {
        playSoundUI();
        nivelActual = nivel;
        
        if(nivel < saveGame.nivelAlcanzado)
        {
            uicontroller.setTextReleaseLevel(
            saveGame.movimientos[nivel],
            saveGame.tiempo[nivel],
            saveGame.eficiencia[nivel]);
        } else
        {
            uicontroller.setTextReleaseLevel(
            0,
            0,
            0);
        }
        
    }
    public void levelCompleteNotify()
    {
        nivelActual++;


        StartCoroutine(AnimationNextLevel());
    }
    private IEnumerator AnimationNextLevel()
    {
        yield return StartCoroutine(uicontroller.showBlack());
        uicontroller.ResetLevel();
        setNewGame();
        PlaySongInGame();

        yield return StartCoroutine(uicontroller.hideBlack());

    }
    public int getTypeLevel()
    {
        return game.gameNiveles.niveles[nivelActual].type;
    }
    public void setNewGame()
    {
        uicontroller.startNivel(
            game.bordes[game.gameNiveles.niveles[nivelActual].type],
            game.contorno[game.gameNiveles.niveles[nivelActual].type],
            game.isCenterLevels[game.gameNiveles.niveles[nivelActual].type],
            game.isTopLevels[game.gameNiveles.niveles[nivelActual].type],
            nivelActual);
        tituloNivel.text = "Nivel " + (nivelActual + 1).ToString();
        game.clear();

        // Se copia una instancia del nivel actual
        game.createGame(nivelActual);

        // Se inicia el nivel
        game.Play();
    }
    public void saveLevelComplete(int movimientos, float duracion, float eficiencia)
    {
        if(nivelActual == saveGame.nivelAlcanzado)
        {
            saveGame.nivelAlcanzado++;
            saveGame.tiempo.Add(duracion);
            saveGame.movimientos.Add(movimientos);
            saveGame.eficiencia.Add(eficiencia);
        } else if(nivelActual < saveGame.nivelAlcanzado)
        {
            if(movimientos > saveGame.movimientos[nivelActual])
            {
                saveGame.movimientos[nivelActual] = movimientos;
            }
            if (duracion > saveGame.tiempo[nivelActual])
            {
                saveGame.tiempo[nivelActual] = duracion;
            }
            if (eficiencia > saveGame.eficiencia[nivelActual])
            {
                saveGame.eficiencia[nivelActual] = eficiencia;
            }
        }



        saveGameSystem.saveGame(saveGame);
        
    }
    private void nextLevel()
    {
        setNewGame();
    }
    private void readSaveGame()
    {
        saveGame = saveGameSystem.loadGame();
    }

}
