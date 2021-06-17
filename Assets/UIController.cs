using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public delegate void starNivel(int numNivel);
public class UIController : MonoBehaviour
{
    public GameObject cardComplete; 
    public GameObject cardActual;
    public GameObject cardLocked;
    public GameObject markActualLevel;

    public EventHandler<int> loadLevel;

    public GameObject levelsContainer;
    private GameObject mark;


    public RectTransform panelTextosPreNivel;
    // Start is called before the first frame update

    public void Start()
    {
        levelsContainer.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
    }
    public void updateLevels(GameObject[] niveles, int saveGame)
    {
        clearCards();
        int posX; 
        mark = GameObject.Instantiate(markActualLevel, levelsContainer.transform);
        mark.GetComponent<RectTransform>().anchoredPosition = new Vector2(74 + (135 * 5), 0);
        for (int i = 0; i<niveles.Length; i++)
        {

            if (i < 5)
            {
                GameObject obj = GameObject.Instantiate(cardComplete,levelsContainer.transform);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(74+(135*i),0);
                obj.GetComponent<RectTransform>().GetChild(1).GetComponent<Text>().text = "Level " + (i+1).ToString();
                int x = i;
                obj.GetComponent<Button>().onClick.AddListener(() => notificarNivel(x));
            } else if(i==5)
            {
                
                GameObject obj = GameObject.Instantiate(cardActual, levelsContainer.transform);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(74 + (135 * i), 0);
                obj.GetComponent<RectTransform>().localScale *= 1.06f; 
                obj.GetComponent<RectTransform>().GetChild(1).GetComponent<Text>().text = "Level " + (i + 1).ToString();
                int x = i;
                obj.GetComponent<Button>().onClick.AddListener(() => notificarNivel(x));


            } else
            {

                GameObject obj = GameObject.Instantiate(cardLocked, levelsContainer.transform);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(74 + (135 * i), 0);
                obj.GetComponent<RectTransform>().GetChild(1).GetComponent<Text>().text = "Level " + (i + 1).ToString();
                obj.GetComponent<Button>().interactable = false;
            }
        }
    }

    private void clearCards()
    {
        foreach(Transform child in levelsContainer.transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    public void setTextReleaseLevel(string text1, string text2, string text3)
    {
        panelTextosPreNivel.GetChild(0).GetComponent<Text>().text = text1;

        panelTextosPreNivel.GetChild(1).GetComponent<Text>().text = text2;

        panelTextosPreNivel.GetChild(2).GetComponent<Text>().text = text3;
    }

    public void startNivel()
    {
        StartCoroutine(showhidePanelNiveles());
    }

    private IEnumerator showhidePanelNiveles()
    {
        CanvasGroup canvasGroup = GameObject.Find("panelInicial").GetComponent<CanvasGroup>();

        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
        GameObject.Find("panelInicial").SetActive(false);
    }
    private void notificarNivel(int nivel)
    {
        loadLevel?.Invoke(this, nivel);
    }

    
}
