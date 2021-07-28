using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public delegate void starNivel(int numNivel);

public delegate void countStats();

public delegate void finishCount();
public delegate void frameStats();
public class UIController : MonoBehaviour
{
    public GameObject cardComplete;
    public GameObject cardActual;
    public GameObject cardLocked;
    public GameObject markActualLevel;

    public EventHandler<int> loadLevel;

    public event countStats eventStats;
    public event finishCount finishStats;
    public event frameStats countStats;

    public GameObject panelInitial;


    public GameObject questionPanel;
    public GameObject panelInGame;
    public GameObject panelLevelComplete;
    private GameObject mark;

    [Header("Panel inicial")]
    public RectTransform panelTextosPreNivel;
    public RectTransform centroApoyoRotable;
    public ScrollRect scrollNiveles;
    public GameObject levelsContainer;
    private RectTransform rectTransformlevels;
    public AnimationCurve curvaScroll;
    [Header("Guias")]
    public GameObject guias;

    [Header("Mascaras level complete")]
    public SpriteMask maskEffectLevelCompleteBorder;
    public SpriteMask maskEffectLevelCompleteLight;
    public Transform colorfield;
    public Transform lightfield;
    private Vector3 initialPoscolorField;
    private Vector3 initialPoslightField;

    [Header("Textos de estadisticas finales")]
    public TextMeshProUGUI textotitulo;
    public TextMeshProUGUI textoMovimientos;
    public TextMeshProUGUI textoTiempo;
    public TextMeshProUGUI textoEficiencia;
    public TextMeshProUGUI textocalificacion;


    [Header("Panel negro de las transiciones")]
    public GameObject blackground;
    private CanvasGroup canvasGroupBlackground;

    public void Start()
    {
        levelsContainer.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        rectTransformlevels = levelsContainer.GetComponent<RectTransform>();
        panelInitial.SetActive(false);
        panelLevelComplete.SetActive(false);
        panelInGame.SetActive(false);
        questionPanel.SetActive(false);
        

        canvasGroupBlackground = blackground.GetComponent<CanvasGroup>();
        canvasGroupBlackground.alpha = 0;
        initialPoscolorField = colorfield.position;
        initialPoslightField = lightfield.position;

        guias.SetActive(false);
    }
    public void mostrarPaneles()
    {
        panelInitial.SetActive(true);
        panelInGame.SetActive(true);
    }
    public void ocultarPaneles()
    {
        panelInitial.SetActive(false);
        panelInGame.SetActive(false);
    }
    public void updateLevels(GameObject[] niveles, int saveGame)
    {
        clearCards();
        mark = GameObject.Instantiate(markActualLevel, levelsContainer.transform);
        mark.GetComponent<RectTransform>().anchoredPosition = new Vector2(74 + (135 * saveGame), 0);
        for (int i = 0; i < niveles.Length; i++)
        {

            if (i < saveGame)
            {
                GameObject obj = GameObject.Instantiate(cardComplete, levelsContainer.transform);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(74 + (135 * i), 0);
                obj.GetComponent<RectTransform>().GetChild(1).GetComponent<TextMeshProUGUI>().text = "Level " + (i + 1).ToString();
                int x = i;
                obj.GetComponent<Button>().onClick.AddListener(() => notificarNivel(x));
            } else if (i == saveGame)
            {

                GameObject obj = GameObject.Instantiate(cardActual, levelsContainer.transform);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(74 + (135 * i), 0);
                obj.GetComponent<RectTransform>().localScale *= 1.06f;
                obj.GetComponent<RectTransform>().GetChild(1).GetComponent<TextMeshProUGUI>().text = "Level " + (i + 1).ToString();
                int x = i;
                obj.GetComponent<Button>().onClick.AddListener(() => notificarNivel(x));


            } else
            {

                GameObject obj = GameObject.Instantiate(cardLocked, levelsContainer.transform);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(74 + (135 * i), 0);
                obj.GetComponent<RectTransform>().GetChild(1).GetComponent<TextMeshProUGUI>().text = "Level " + (i + 1).ToString();
                obj.GetComponent<Button>().interactable = false;
            }
        }
    }
    private void clearCards()
    {


        foreach (Transform child in levelsContainer.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in levelsContainer.transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }

    }
    public void setTextReleaseLevel(int movimientos, float tiempo, float eficiencia)
    {
        TimeSpan t = TimeSpan.FromSeconds(tiempo);

        panelTextosPreNivel.GetChild(1).GetComponent<TextMeshProUGUI>().text = movimientos.ToString();
        panelTextosPreNivel.GetChild(2).GetComponent<TextMeshProUGUI>().text = string.Format("{0:D2}m:{1:D2}s",
                        t.Minutes,
                        t.Seconds);


        panelTextosPreNivel.GetChild(3).GetComponent<TextMeshProUGUI>().text = Mathf.Round(eficiencia).ToString() + "%";
    }
    public void SnapTo(Vector2 target)
    {
        Canvas.ForceUpdateCanvases();

        rectTransformlevels.anchoredPosition =(Vector2)scrollNiveles.transform.InverseTransformPoint(rectTransformlevels.position) - (Vector2)scrollNiveles.transform.InverseTransformPoint(target);
    }
    public void startNivel(Sprite border, Sprite contorno, bool iscenter, bool istop, int numeroNivel)
    {
        textotitulo.text = "¡Nivel " + (numeroNivel + 1).ToString() + " completado!";
        
        maskEffectLevelCompleteBorder.sprite = border;

        maskEffectLevelCompleteLight.sprite = contorno;

        
        StartCoroutine(showhidePanelNiveles(false));
    }
    private IEnumerator showhidePanelNiveles(bool show)
    {
        panelInitial.SetActive(true);
        CanvasGroup canvasGroup = panelInitial.GetComponent<CanvasGroup>();
        if (show)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime;
                yield return null;
            }

            questionPanel.SetActive(false);
        } else
        {

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime;
                yield return null;
            }
            panelInitial.SetActive(false);
        }

    }
    private void notificarNivel(int nivel)
    {
        StartCoroutine(GirarCentroApoyo());
        mark.GetComponent<RectTransform>().anchoredPosition = new Vector2(74 + (135 * nivel), 0);
        //SnapTo(new Vector2(74 + (135 * nivel), 0));

        //rectTransformlevels.anchoredPosition = new Vector2((Screen.width / 2) - (135 * nivel) - 74, 0);
        StartCoroutine(MoverScrollNiveles(nivel));
        loadLevel?.Invoke(this, nivel);
    }
    private IEnumerator MoverScrollNiveles(int target)
    {
        float posX = rectTransformlevels.anchoredPosition.x;
        float targetX = (Screen.width / 2) - (135 * target) - 74;
        float diferenceTarget = Mathf.Abs(targetX - posX);

        float contador = 0;

        if (targetX > posX)
        {
            while (diferenceTarget > contador)
            {
                float valorInstante = curvaScroll.Evaluate(contador / diferenceTarget);
                rectTransformlevels.anchoredPosition = new Vector2(posX + diferenceTarget * valorInstante ,0);
                contador += Time.deltaTime * 500;
                yield return null;
                
            }
            
        } else if(targetX < posX)
        {
            while (diferenceTarget > contador)
            {
                float valorInstante = curvaScroll.Evaluate(contador / diferenceTarget);

                rectTransformlevels.anchoredPosition = new Vector2(posX - (diferenceTarget * valorInstante), 0);

                contador += Time.deltaTime * 500;
                yield return null;
            }
        }

    }
    private IEnumerator GirarCentroApoyo()
    {
        float angle = 0;

        Vector3 angulosAnteriores = centroApoyoRotable.eulerAngles;

        while (angle < 90)
        {
            centroApoyoRotable.eulerAngles = new Vector3(0,0, angulosAnteriores.z + angle);
            angle+= Time.deltaTime *350;
            yield return null;
        }
        centroApoyoRotable.eulerAngles = angulosAnteriores + new Vector3(0,0,90);
    }
    public void mostrarOcultarGuias()
    {
        if (guias.activeInHierarchy)
        {
            guias.SetActive(false);
        } else
        {
            guias.SetActive(true);
        }
    }
    public void levelComplete(float movimientos, float duracion, float eficiencia)
    {


        StartCoroutine(animacionEstadisticas(movimientos, duracion, eficiencia));

        StartCoroutine(animacionLightLevelComplete());
    }
    public void showQuestionPanel()
    {
        GameObject.Find("GameController").GetComponent<Game>().disableCenters();
        questionPanel.SetActive(true);
    }
    public void hideQuestionPanel()
    {
        GameObject.Find("GameController").GetComponent<Game>().enableCenters();
        questionPanel.SetActive(false);
    }
    public void backToMain()
    {
        StartCoroutine(showhidePanelNiveles(true));
    }
    public void ResetLevel()
    {
        panelLevelComplete.SetActive(false);
        colorfield.position = initialPoscolorField;
        lightfield.position = initialPoslightField;
        textocalificacion.text = "";
        textoMovimientos.text = "";
        textoTiempo.text = "";
        textoEficiencia.text = "";
    }
    private IEnumerator animacionEstadisticas(float movimientos, float duracion, float eficiencia)
    {
        textocalificacion.text = "";
        yield return new WaitForSeconds(0.3f);
        float posXlight = lightfield.position.x;
        while (posXlight < 8)
        {
            lightfield.position += new Vector3(Time.deltaTime * 23, 0, 0);
            posXlight = lightfield.position.x;
            yield return null;
        }
        panelLevelComplete.SetActive(true);
        float mov = 0;
        float dur = 0;
        float efi = 0;

        eventStats?.Invoke();
        float delay = movimientos < 20 ? 0.02f : 1 / movimientos;
        while (mov < movimientos)
        {
            mov++;
            textoMovimientos.text = mov.ToString();
            yield return new WaitForSeconds(delay);
        }

        countStats?.Invoke();
        yield return new WaitForSeconds(0.35f);
        eventStats?.Invoke();
        delay = duracion < 20 ? 0.02f : 1 / duracion;
        while (dur < duracion)
        {

            dur++;
            TimeSpan t = TimeSpan.FromSeconds(dur);

            textoTiempo.text = string.Format("{0:D2}m:{1:D2}s",
                            t.Minutes,
                            t.Seconds);
            yield return new WaitForSeconds(delay);
        }
        countStats?.Invoke();
        yield return new WaitForSeconds(0.35f);
        eventStats?.Invoke();

        while (efi < eficiencia)
        {

            efi++;
            textoEficiencia.text = efi.ToString() + "%";
            yield return new WaitForSeconds(0.02f);

            
            
        }
        finishStats?.Invoke(); 
        textocalificacion.text = calcularCalificacionString(eficiencia);

    }
    private IEnumerator animacionLightLevelComplete()
    {
        float posX = colorfield.position.x;
        while (posX > 0)
        {
            colorfield.position -= new Vector3(Time.deltaTime * 23, 0, 0);
            posX = colorfield.position.x;
            yield return null;
        }
        
    }

    /// <summary>
    /// Muestra color negro en la pantalla
    /// </summary>
    /// <returns>Null mientras el color no sea totalmente opaco</returns>
    public IEnumerator showBlack()
    {
        blackground.SetActive(true);
        while(canvasGroupBlackground.alpha < 1)
        {
            canvasGroupBlackground.alpha += Time.deltaTime;


            yield return null;
        }


    }

    /// <summary>
    /// Oculta el color negro de la pantalla
    /// </summary>
    /// <returns>Null mientras el color no sea totalmente transparente</returns>
    public IEnumerator hideBlack()
    {
        while (canvasGroupBlackground.alpha > 0)
        {
            canvasGroupBlackground.alpha -= Time.deltaTime;

            yield return null;
        }

        blackground.SetActive(false);

    }

    private string calcularCalificacionString(float calificacion)
    {
        if (calificacion == 100)
        {
            return "Perfect!";
        } else if(calificacion > 95)
        {
            return "Muy bien!";
        }
        else if (calificacion > 85)
        {
            return "bien!";
        }
        else if (calificacion > 65)
        {
            return "Regular, pero bien";
        }
        else if (calificacion > 55)
        {
            return "Maso menos";
        }
        else if (calificacion > 45)
        {
            return "Nada que decir";
        }
        else if (calificacion > 25)
        {
            return "Haz tenido suerte";
        } else
        {
            return "Casi no acabas";
        }
    }

}
