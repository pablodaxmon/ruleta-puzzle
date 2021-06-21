using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Custom Debug
/// </summary>
public static class CDebug
{
    public static void Log()
    {
        Debug.Log(" <b> Test here </b> ");

    }
    public static void LogRed()
    {
        Debug.Log(" <b><color=red> Test here </color></b> ");
    }
    public static void LogBlue()
    {
        Debug.Log(" <b><color=blue> Test here </color></b> ");
    }
    public static void LogGreen()
    {
        Debug.Log(" <b><color=green> Test here </color></b> ");
    }
    public static void LogRed(string message)
    {
        Debug.Log(" <b><color=red> " + message + "</color></b> ");
    }
    public static void LogBlue(string message)
    {
        Debug.Log(" <b><color=blue>" + message + "</color></b> ");
    }
    public static void LogGreen(string message)
    {
        Debug.Log(" <b><color=green>" + message + "</color></b> ");
    }
    public static void Log(object objA) {
        Debug.Log("Parametro A: " + objA);
    }
    public static void Log(object objA,object objB)
    {
        Debug.Log("Parametro A: " + objA + " || " + "Parametro B: " + objB);
    }
    public static void Log(string textA, object objA)
    {
        Debug.Log(textA + " " + objA );
    }
    public static void Log(string textA, object objA, string textB, object objB)
    {
        Debug.Log(textA+" "+objA+ " || " +textB+" "+objB);
    }
    public static void Log(string textA, object objA, string textB, object objB, string textC, object objc)
    {
        Debug.Log(textA + " " + objA + " || " + textB + " " + objB + " || " + textC + " " + objc);
    }
    public static void Log(string textA, object objA, string textB, object objB, string textC, object objC, string textD, object objD)
    {
        Debug.Log(textA + " " + objA + " || " + textB + " " + objB + " || " + textC + " " + objC + " || " + textD + " " + objD);
    }
}
