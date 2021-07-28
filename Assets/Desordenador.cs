using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Desordenador : MonoBehaviour
{
    public GameObject desordenandoAlert;
    Game game;
    private int puntaje;
    private float indiceDeDesorden;
    private int tipo;
    private int[][] preMovimientos = new int[14][]
    {
        new int[]{1,0,1}, // tipo 0  
        new int[]{1,2,0,3,2}, // tipo 1
        new int[]{2,3,4,0,2}, // tipo 2
        new int[]{3,1,4,3}, // tipo 3
        new int[]{4,3,1,2}, // tipo 4
        new int[]{3,4,6,2,6}, // tipo 5
        new int[]{0,2,3,4,5,3,0,2}, // tipo 6
        new int[]{0,1,2,3,4,5,4}, // tipo 7
        new int[]{0,1,2,3,4,7,4,2,3,4}, // tipo 8
        new int[]{0,1,2,3,4,5,6,7,8,2,4,6}, // tipo 9
        new int[]{1,2,3,4,5,6,3,2}, // tipo 10
        new int[]{1,3,5,6,7,8,9,10}, // tipo 11
        new int[]{0,2,3,4,5,6,7,8,10,8,3}, // tipo 12
        new int[]{0,2,3,4,5,6,8,9,10,12,14,10,8,3,5}, // tipo 13
    };
    private int[][] preMovimientosInterior = new int[14][]
    {
        // 0 circulo entero, 1 circulo pequeño
        new int[]{0,0,0},   // tipo 0
        new int[]{0,0,0,0,1},   // tipo 1
        new int[]{0,0,0,0,1},   // tipo 2
        new int[]{0,0,0,1},   // tipo 3
        new int[]{0,0,0,0},   // tipo 4
        new int[]{0,0,0,0,1},   // tipo 5
        new int[]{0,0,0,0,0,1,1,1},   // tipo 6
        new int[]{0,0,0,0,0,0,1},   // tipo 7
        new int[]{0,0,0,0,0,0,1,1,1,1},   // tipo 8
        new int[]{0,0,0,0,0,0,0,0,0,1,1,1},   // tipo 9
        new int[]{0,0,0,0,0,0,0,0,0,0,1,1,1},   // tipo 10
        new int[]{0,0,0,0,0,0,0,0,0,0,0},   // tipo 11
        new int[]{0,0,0,0,0,0,0,0,1,1,1},   // tipo 12
        new int[]{0,0,0,0,0,0,0,0,0,0,1,1,1,1,1},   // tipo 13
    };

    private float[] indicesDesorden = new float[14]
    {
        1.1f,
        1.2f,
        1.3f,
        1.6f,
        1.7f,
        2.4f,
        2f,
        2.2F,
        2.2F,
        2.2F,
        2.4f,
        2.4f,
        2.3f,
        1.2f
    };

    public void Start()
    {
        game = GetComponent<Game>();
    }

    public void startDesorden(List<GameObject> centros)
    {
        StartCoroutine(desordenar(centros));
    }

    private IEnumerator desordenar(List<GameObject> centros)
    {
        string textAlert = "Desordenando...";
        desordenandoAlert.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = textAlert;
        desordenandoAlert.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        int circuloParaGirar;
        int interiorOExterior;
        int cantidadGiro;
        float velocidadGiro = 5;
        float aumentoVelocidadGiro;
        puntaje = game.Puntaje;
        tipo = GetComponent<MainGame>().getTypeLevel();
        indiceDeDesorden = indicesDesorden[tipo];
        if (indiceDeDesorden <= 1)
        {
            indiceDeDesorden = 1.05f;
        }
        float indice = puntaje / indiceDeDesorden;


        for(int i = 0; i < preMovimientos[tipo].Length; i++)
        {
            
            yield return StartCoroutine(centros[preMovimientos[tipo][i]].GetComponent<CentroControlle>().girandoCirculo(90, 0.1f, preMovimientosInterior[tipo][i]));
        
        
        }
        
        while (puntaje > indice)
        {
            
            CentroControlle.rotando = true; 
            puntaje = game.Puntaje;

            ///elegimos un centro
            circuloParaGirar = Random.Range(0, centros.Count);
            ///elegimos centro exterior o interior
            interiorOExterior = Random.Range(0, 2);
            ///si salio interior elegimos cuanto va girar

            cantidadGiro = Random.Range(0, 5);

            velocidadGiro += 1;
            //aumentoVelocidadGiro = 820 + 20 * Mathf.Log10(velocidadGiro + 0.15f);

            yield return StartCoroutine(centros[circuloParaGirar].GetComponent<CentroControlle>().girandoCirculo(90, 0.1f, interiorOExterior));


        }

        yield return new WaitForSeconds(0.1f);
        desordenandoAlert.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "LISTO!";
        yield return new WaitForSeconds(0.8f);

        CentroControlle.rotando = false;
        game.isPlaying = true;

        desordenandoAlert.SetActive(false);
    }
}
