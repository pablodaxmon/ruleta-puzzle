using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UIStatusGameController : MonoBehaviour
{
    private GameManager gameManager;
    private Game game;

    public Text textoPuntaje;
    public Slider sliderPuntaje;

    private int maxPoints;
    private int points;
    private float sliderChangeVelocity = 0f;

    public GameObject panelLevelComplete;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        game = GetComponent<Game>();
        panelLevelComplete.SetActive(false);
    }

    public void setMaxPoints(int points)
    {
        maxPoints = points;
        sliderPuntaje.minValue = 0;
        sliderPuntaje.maxValue = maxPoints;
    }

    public void Update()
    {
        sliderPuntaje.value = Mathf.SmoothDamp(sliderPuntaje.value, points, ref sliderChangeVelocity, 0.1f); ;
    }

    public void updatePoints(int point)
    {
        points = point;
        float porcentaje = (100 * points) / maxPoints;
        float porcentajeRedondeado = Mathf.Round(porcentaje);
        textoPuntaje.text = porcentajeRedondeado.ToString() + "%";
    }

    public void levelComplete()
    {
        panelLevelComplete.SetActive(true);
    }



}
