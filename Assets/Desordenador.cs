using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desordenador : MonoBehaviour
{
    Game game;
    private int puntaje;
    private float indiceDeDesorden;
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
        yield return new WaitForSeconds(0.3f);
        int circuloParaGirar;
        int interiorOExterior;
        int cantidadGiro;
        float velocidadGiro = 5;
        float aumentoVelocidadGiro;
        puntaje = game.Puntaje;
        indiceDeDesorden = GetComponent<MainGame>().getIndiceDeDesorden();
        if(indiceDeDesorden == 1)
        {
            indiceDeDesorden = 1.1f;
        }
        float indice = puntaje / indiceDeDesorden;

        
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
            aumentoVelocidadGiro = 820 + 20 * Mathf.Log10(velocidadGiro + 0.15f);

            yield return StartCoroutine(centros[circuloParaGirar].GetComponent<CentroControlle>().girandoCirculo(90, aumentoVelocidadGiro, interiorOExterior));


        }
        CentroControlle.rotando = false;
        game.isPlaying = true;
    }
}
