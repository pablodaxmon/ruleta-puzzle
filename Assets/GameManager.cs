using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class GameManager : MonoBehaviour
{
    public Color negroTransparente = new Color(0,0,0,0);
    List<GameObject> objetosParaEliminar = new List<GameObject>();

    public int calculatePoints(List<GameObject> centros, List<GameObject> rotables)
    {

        int puntaje = 0;
        estructurarColores(centros,rotables);

        foreach(GameObject centro in centros)
        {
            ColorMapping colorMapping = centro.GetComponent<ColorMapping>();

            

            if (colorMapping.getColorDerecha(distanciaColor.CERCANO) != negroTransparente)
            {
                if (centro.GetComponentInChildren<SpriteRenderer>().color == colorMapping.getColorDerecha(distanciaColor.CERCANO))
                {
                    puntaje++;
                }
                if (colorMapping.getColorDerecha(distanciaColor.CERCANO) == colorMapping.getColorDerecha(distanciaColor.MEDIO))
                {
                    puntaje++;
                }
                if (colorMapping.getColorDerecha(distanciaColor.MEDIO) == colorMapping.getColorDerecha(distanciaColor.LEJANO))
                {
                    puntaje++;
                }
                
                if (colorMapping.getColorDerecha(distanciaColor.LEJANO) == colorMapping.getColorDerecha(distanciaColor.CENTROADYACENTE))
                {
                    puntaje++;
                }

            }

            if (colorMapping.getColorArriba(distanciaColor.CERCANO) != negroTransparente)
            {
                if (centro.GetComponentInChildren<SpriteRenderer>().color == colorMapping.getColorArriba(distanciaColor.CERCANO))
                {
                    puntaje++;
                }
                if (colorMapping.getColorArriba(distanciaColor.CERCANO) == colorMapping.getColorArriba(distanciaColor.MEDIO))
                {
                    puntaje++; 
                    
                }
                if (colorMapping.getColorArriba(distanciaColor.MEDIO) == colorMapping.getColorArriba(distanciaColor.LEJANO))
                {
                    puntaje++;
                }
                
                if (colorMapping.getColorArriba(distanciaColor.LEJANO) == colorMapping.getColorArriba(distanciaColor.CENTROADYACENTE))
                {
                    puntaje++;
                }

            }


            if (colorMapping.getColorIzquierda(distanciaColor.CERCANO) != negroTransparente)
            {
                if (centro.GetComponentInChildren<SpriteRenderer>().color == colorMapping.getColorIzquierda(distanciaColor.CERCANO))
                {
                    puntaje++;
                }
                if (colorMapping.getColorIzquierda(distanciaColor.CERCANO) == colorMapping.getColorIzquierda(distanciaColor.MEDIO))
                {
                    puntaje++; 
                    
                }
                if (colorMapping.getColorIzquierda(distanciaColor.MEDIO) == colorMapping.getColorIzquierda(distanciaColor.LEJANO))
                {
                    puntaje++;
                }
                
                if (colorMapping.getColorIzquierda(distanciaColor.CENTROADYACENTE) != Color.black)
                {
                    if (colorMapping.getColorIzquierda(distanciaColor.LEJANO) == colorMapping.getColorIzquierda(distanciaColor.CENTROADYACENTE))
                    {
                        puntaje++;
                    }
                }

            }

            if (colorMapping.getColorAbajo(distanciaColor.CERCANO) != negroTransparente)
            {
                if (centro.GetComponentInChildren<SpriteRenderer>().color == colorMapping.getColorAbajo(distanciaColor.CERCANO))
                {
                    puntaje++;
                }
                if (colorMapping.getColorAbajo(distanciaColor.CERCANO) == colorMapping.getColorAbajo(distanciaColor.MEDIO))
                {
                    puntaje++;

                }
                if (colorMapping.getColorAbajo(distanciaColor.MEDIO) == colorMapping.getColorAbajo(distanciaColor.LEJANO))
                {
                    puntaje++;
                }
                
                if (colorMapping.getColorAbajo(distanciaColor.LEJANO) == colorMapping.getColorAbajo(distanciaColor.CENTROADYACENTE))
                {
                    puntaje++;
                }

            }

        }
        return puntaje;
    }

    // proporciona los colores al mapa de color para calcular el puntaje
    private void estructurarColores(List<GameObject> centros, List<GameObject> rotables)
    {
        // angulo del rotable respecto al centro
        List<GameObject> rotablesContables = new List<GameObject>(rotables);
        float angulo;
        float distancia;
         // Relacionar todos los centros con cada rotable
        foreach (GameObject centro in centros)
        {
            
            foreach (GameObject rotable in rotablesContables)
            {
                distancia = Vector2.Distance(centro.transform.position, rotable.transform.position);

                // angulo del rotable respecto al centro
                angulo = Mathf.Round(Vector2.SignedAngle(centro.transform.position - rotable.transform.position, Vector2.right * -1));

                /*si el angulo es 0, apunta a la derecha, si es 90 apunta abajo, -90 arriba y 180 o -180 izquierda.*/
                if (angulo == 0)
                {
                    //si la distancia es 1.25 cercano, 1.50 medio y 1.75 lejano.
                    if (distancia > 1.24f  && distancia < 1.26f )
                    {
                        //proporcionamos al mapa de color el color del rotable actual.
                        centro.GetComponent<ColorMapping>().setColorDerecha(distanciaColor.CERCANO, rotable.GetComponent<SpriteRenderer>().color);
                        //añadimos el rotable a la lista de rotables para luego eliminarlo y no volver a contarlo en el puntaje
                        objetosParaEliminar.Add(rotable);
                    }
                    else if (distancia > 1.49f && distancia < 1.51f)
                    {
                        centro.GetComponent<ColorMapping>().setColorDerecha(distanciaColor.MEDIO, rotable.GetComponent<SpriteRenderer>().color);

                        centro.GetComponent<ColorMapping>().setObjectMedioDerecha(rotable.transform);
                        objetosParaEliminar.Add(rotable);
                    }
                    else if (distancia > 1.74f && distancia < 1.76f)
                    {
                        centro.GetComponent<ColorMapping>().setColorDerecha(distanciaColor.LEJANO, rotable.GetComponent<SpriteRenderer>().color);
                        objetosParaEliminar.Add(rotable);
                    }
                    
                }
                /* repite con todos los angulos*/
                else if (angulo == -90)
                {
                    if (distancia > 1.24f && distancia < 1.26f)
                    {
                        centro.GetComponent<ColorMapping>().setColorArriba(distanciaColor.CERCANO, rotable.GetComponent<SpriteRenderer>().color);
                        objetosParaEliminar.Add(rotable);
                    }
                    else if (distancia > 1.49f && distancia < 1.51f)
                    {
                        centro.GetComponent<ColorMapping>().setColorArriba(distanciaColor.MEDIO, rotable.GetComponent<SpriteRenderer>().color);
                        centro.GetComponent<ColorMapping>().setObjectMedioArriba(rotable.transform);
                        objetosParaEliminar.Add(rotable);
                    }
                    else if (distancia > 1.74f && distancia < 1.76f)
                    {
                        centro.GetComponent<ColorMapping>().setColorArriba(distanciaColor.LEJANO, rotable.GetComponent<SpriteRenderer>().color);
                        objetosParaEliminar.Add(rotable);
                    }
                }
                else if (angulo == 180 || angulo == -180)
                {
                    if (distancia > 1.24f && distancia < 1.26f)
                    {
                        centro.GetComponent<ColorMapping>().setColorIzquierda(distanciaColor.CERCANO, rotable.GetComponent<SpriteRenderer>().color);
                        objetosParaEliminar.Add(rotable);
                    }
                    else if (distancia > 1.49f && distancia < 1.51f)
                    {
                        centro.GetComponent<ColorMapping>().setColorIzquierda(distanciaColor.MEDIO, rotable.GetComponent<SpriteRenderer>().color);
                        centro.GetComponent<ColorMapping>().setObjectMedioIzquierda(rotable.transform);
                        objetosParaEliminar.Add(rotable);
                    }
                    else if (distancia > 1.74f && distancia < 1.76f)
                    {
                        centro.GetComponent<ColorMapping>().setColorIzquierda(distanciaColor.LEJANO, rotable.GetComponent<SpriteRenderer>().color);
                        objetosParaEliminar.Add(rotable);
                    }
                }
                else if (angulo == 90)
                {
                    if (distancia > 1.24f && distancia < 1.26f)
                    {
                        centro.GetComponent<ColorMapping>().setColorAbajo(distanciaColor.CERCANO, rotable.GetComponent<SpriteRenderer>().color);
                        objetosParaEliminar.Add(rotable);
                    }
                    else if (distancia > 1.49f && distancia < 1.51f)
                    {
                        centro.GetComponent<ColorMapping>().setColorAbajo(distanciaColor.MEDIO, rotable.GetComponent<SpriteRenderer>().color);
                        centro.GetComponent<ColorMapping>().setObjectMedioAbajo(rotable.transform);
                        objetosParaEliminar.Add(rotable);
                    }
                    else if (distancia > 1.74f && distancia < 1.76f)
                    {
                        centro.GetComponent<ColorMapping>().setColorAbajo(distanciaColor.LEJANO, rotable.GetComponent<SpriteRenderer>().color);
                        objetosParaEliminar.Add(rotable);
                    }
                }
                
            }

            // por cada centro obtener el color del centro adyacente
            foreach (GameObject otroCentro in centros)
            {
                if (Mathf.Round(Vector2.Distance(otroCentro.transform.position, centro.transform.position)) == 3)
                {
                    angulo = Vector2.SignedAngle(centro.transform.position - otroCentro.transform.position, Vector2.right * -1);
                    if (angulo == 0)
                    {

                        centro.GetComponent<ColorMapping>().setColorDerecha(distanciaColor.CENTROADYACENTE, otroCentro.GetComponentInChildren<SpriteRenderer>().color);
                    }
                    else if (angulo == -90)
                    {
                        centro.GetComponent<ColorMapping>().setColorArriba(distanciaColor.CENTROADYACENTE, otroCentro.GetComponentInChildren<SpriteRenderer>().color);
                    }
                    else if (angulo == 180 || angulo == -180)
                    {
                        centro.GetComponent<ColorMapping>().setColorIzquierda(distanciaColor.CENTROADYACENTE, otroCentro.GetComponentInChildren<SpriteRenderer>().color);
                    }
                    else if (angulo == 90)
                    {
                        centro.GetComponent<ColorMapping>().setColorAbajo(distanciaColor.CENTROADYACENTE, otroCentro.GetComponentInChildren<SpriteRenderer>().color);
                    }
                }
            }
            foreach (GameObject objeto in objetosParaEliminar)
            {
                rotablesContables.Remove(objeto);
            }
            objetosParaEliminar.Clear();
        }

    }

}
