using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public delegate void starNivel(int numNivel);
public class UIController : MonoBehaviour
{
    public EventHandler<int> loadLevel;
    // Start is called before the first frame update
    public void updateLevels(GameObject[] niveles, int saveGame)
    {

    }

    private void notificarNivel(int nivel)
    {
        loadLevel?.Invoke(this, nivel);
    }

    
}
