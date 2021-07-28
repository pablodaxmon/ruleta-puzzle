using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatusGameController : MonoBehaviour
{
    private GameManager gameManager;
    private Game game;

    public TextMeshProUGUI textoPuntaje;
    public Slider sliderPuntaje;

    private int maxPoints;
    private int points;
    private float sliderChangeVelocity = 0f;

    public void Start()
    {
        gameManager = GetComponent<GameManager>();
        game = GetComponent<Game>();
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

   


}
