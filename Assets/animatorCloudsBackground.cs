using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animatorCloudsBackground : MonoBehaviour
{
    public int velocidadNube = 300;
    [Header("Valores entre 1 o -1")]
    public int direccionNube = 1;
    public void Update()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition += direccionNube * new Vector2(1,0) * Time.deltaTime * velocidadNube;
        if(gameObject.GetComponent<RectTransform>().anchoredPosition.x* direccionNube > Screen.width)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width*-1* direccionNube, gameObject.GetComponent<RectTransform>().anchoredPosition.y);
        }
    }



}
