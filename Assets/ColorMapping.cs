using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum distanciaColor
{
    CERCANO,
    MEDIO,
    LEJANO,
    CENTROADYACENTE
}
public struct ordenColores
{
    public Color cercano;
    public Color medio;
    public Color lejano;
    public Color centroAdyacente;
    public Transform objectMedio { get; set ;}
}

public class ColorMapping : MonoBehaviour
{
    private ordenColores derecha;
    private ordenColores arriba;
    private ordenColores izquierda;
    private ordenColores abajo;





    public void Start()
    {
        derecha = new ordenColores();

        arriba = new ordenColores();

        izquierda = new ordenColores();

        abajo = new ordenColores();

    }
    public void setObjectMedioDerecha(Transform objectMedio)
    {
        derecha.objectMedio = objectMedio;
    }
    public void setObjectMedioArriba(Transform objectMedio)
    {
        arriba.objectMedio = objectMedio;
    }
    public void setObjectMedioIzquierda(Transform objectMedio)
    {
        izquierda.objectMedio = objectMedio;
    }
    public void setObjectMedioAbajo(Transform objectMedio)
    {
        abajo.objectMedio = objectMedio;
    }

    // get objects
    public Transform getObjectMedioDerecha()
    {
        return derecha.objectMedio;
    }
    public Transform getObjectMedioArriba()
    {
        return arriba.objectMedio;
    }
    public Transform getObjectMedioIzquierda()
    {
        return izquierda.objectMedio;
    }
    public Transform getObjectMedioAbajo()
    {
        return abajo.objectMedio;
    }

    public void setColorDerecha(distanciaColor distanciaColor, Color color)
    {
        switch (distanciaColor)
        {
            case distanciaColor.CERCANO:
                derecha.cercano = color;
                break;
            case distanciaColor.MEDIO:
                derecha.medio = color;
                break;
            case distanciaColor.LEJANO:
                derecha.lejano = color;
                break;
            case distanciaColor.CENTROADYACENTE:
                derecha.centroAdyacente = color;
                break;
            default:
                return;

        }
    }
    public void setColorArriba(distanciaColor distanciaColor, Color color)
    {
        switch (distanciaColor)
        {
            case distanciaColor.CERCANO:
                arriba.cercano = color;
                break;
            case distanciaColor.MEDIO:
                arriba.medio = color;
                break;
            case distanciaColor.LEJANO:
                arriba.lejano = color;
                break;
            case distanciaColor.CENTROADYACENTE:
                arriba.centroAdyacente = color;
                break;
            default:
                return;

        }
    }
    public void setColorIzquierda(distanciaColor distanciaColor, Color color)
    {
        switch (distanciaColor)
        {
            case distanciaColor.CERCANO:
                izquierda.cercano = color;
                break;
            case distanciaColor.MEDIO:
                izquierda.medio = color;
                break;
            case distanciaColor.LEJANO:
                izquierda.lejano = color;
                break;
            case distanciaColor.CENTROADYACENTE:
                izquierda.centroAdyacente = color;
                break;
            default:
                return;

        }
    }
    public void setColorAbajo(distanciaColor distanciaColor, Color color)
    {
        switch (distanciaColor)
        {
            case distanciaColor.CERCANO:
                abajo.cercano = color;
                break;
            case distanciaColor.MEDIO:
                abajo.medio = color;
                break;
            case distanciaColor.LEJANO:
                abajo.lejano = color;
                break;
            case distanciaColor.CENTROADYACENTE:
                abajo.centroAdyacente = color;
                break;
            default:
                return;

        }

    }

    public Color getColorDerecha(distanciaColor distanciaColor)
    {
        switch (distanciaColor)
        {
            case distanciaColor.CERCANO:
                return derecha.cercano;
            case distanciaColor.MEDIO:
                return derecha.medio;
            case distanciaColor.LEJANO:
                return derecha.lejano;
            case distanciaColor.CENTROADYACENTE:
                return derecha.centroAdyacente;
            default:
                return Color.white;

        }
    }
    public Color getColorArriba(distanciaColor distanciaColor)
    {
        switch (distanciaColor)
        {
            case distanciaColor.CERCANO:
                return arriba.cercano;
            case distanciaColor.MEDIO:
                return arriba.medio;
            case distanciaColor.LEJANO:
                return arriba.lejano;
            case distanciaColor.CENTROADYACENTE:
                return arriba.centroAdyacente;
            default:
                return Color.white;

        }
    }
    public Color getColorIzquierda(distanciaColor distanciaColor)
    {
        switch (distanciaColor)
        {
            case distanciaColor.CERCANO:
                return izquierda.cercano;
            case distanciaColor.MEDIO:
                return izquierda.medio;
            case distanciaColor.LEJANO:
                return izquierda.lejano;
            case distanciaColor.CENTROADYACENTE:
                return izquierda.centroAdyacente;
            default:
                return Color.white;

        }
    }
    public Color getColorAbajo(distanciaColor distanciaColor)
    {
        switch (distanciaColor)
        {
            case distanciaColor.CERCANO:
                return abajo.cercano;
            case distanciaColor.MEDIO:
                return abajo.medio;
            case distanciaColor.LEJANO:
                return abajo.lejano;
            case distanciaColor.CENTROADYACENTE:
                return abajo.centroAdyacente;
            default:
                return Color.white;

        }

    }

}
