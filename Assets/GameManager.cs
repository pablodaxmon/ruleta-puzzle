using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject[] centros;


    private bool centerIsUp;


    //Calculo giros
    private float baseAngle;
    private float velocitySmoothGiro = 0;
    public void GameStart()
    {
        clearGameObjects();
        getAllCenter();
    }
    private IEnumerator hola()
    {
        yield return new WaitForSeconds(4);
    }


    private void getAllCenter()
    {
        centros = GameObject.FindGameObjectsWithTag("Centro");
    }

    private void clearGameObjects()
    {

    }

}
