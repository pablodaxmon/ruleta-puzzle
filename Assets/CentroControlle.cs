using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// Controla la rotacion 
public class CentroControlle : MonoBehaviour
{
    public EventHandler<GameObject> centroClickDown;
    public EventHandler<GameObject> centroClickUp;
    public EventHandler<GameObject> giroCompleto;


    List<GameObject> objetosRotables;
    List<GameObject> esquinas;
    public AnimationCurve curvaCamara;
    public AnimationCurve animationRotation;
    public float anguloLimiteMinimoCompletarGiro;
    private float baseAngle;
    private bool clickLegal;
    private Vector3 initialtouchposition;
    private float velocitySmoothGiro = 0;
    private float velocitySmoothCompleteGiro = 0;


    public static bool rotando { get; set; }

    public static int progreso;

    public ColorMapping structureColor;

    public void SetStart()
    {

        obtenerObjetosRotables();
        obtenerEsquinas();
    }
    private void obtenerEsquinas()
    {
        esquinas = new List<GameObject>(GameObject.FindGameObjectsWithTag("Esquina"));
    }
    //Se llama a esta funcion el instante que el mouse hizo click en el circulo
    private void OnMouseDown()
    {
        //si ningun circulo esta rotando se ejecutara la instruccion
        if (!rotando)
        {
            //todos los rotables se quedan sin parents
            elimitarParentsDeRotables();

            //posicion del mouse cuando se hizo click
            initialtouchposition = Input.mousePosition;
            
            //posicion de este objeto en la pantalla
            Vector3 dir = Camera.main.WorldToScreenPoint(transform.position);

            //diferencia entre el click y la posicion de este objeto
            dir = Input.mousePosition - dir;

            //angulo del punto donde se hizo click
            baseAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            
            baseAngle -= Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;

            clickLegal = true;

            rotarCirculoPequeño();

            centroClickDown?.Invoke(this, this.gameObject);
        }
    }
    private void OnMouseDrag()
    {
        if (!rotando && Vector2.Distance(initialtouchposition, Input.mousePosition) >= 15 && clickLegal)
        {
            Vector3 dir = Camera.main.WorldToScreenPoint(transform.position);
            dir = Input.mousePosition - dir;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - baseAngle;
            angle /= 90;
            angle = Mathf.Round(angle);
            angle *= 90;
            float smoothangle = Mathf.SmoothDampAngle(transform.eulerAngles.z, angle, ref velocitySmoothGiro, 0.2f);



            transform.rotation = Quaternion.AngleAxis(smoothangle, Vector3.forward);
            /*float angl = transform.eulerAngles.z;
            
            transform.eulerAngles = new Vector3(0,0,angl);*/
        }
    }
    private void OnMouseUp()
    {
        clickLegal = false;
        ///si el click inicial y el click que suelta esta muy pequeño la distancia;
        if (Vector2.Distance(initialtouchposition, Input.mousePosition) < 15)
        {
            if (!rotando)
            {
                rotarCirculoCompleto();

            }

        }
        else
        {


            if (!rotando)
            {
                StartCoroutine(completarGiro());
            }

            ///StartCoroutine(completarGiro(angulocomplementario)); P & Y
        }
    }

    private void rotarCirculoCompleto()
    {
        agregarCirculoEnteroAlTransform();
        StartCoroutine(girarCirculo());
    }

    private void rotarCirculoPequeño()
    {
        agregarCirculoPequeñoAlTRansform();
    }

    private void obtenerObjetosRotables()
    {
        objetosRotables = new List<GameObject>(GameObject.FindGameObjectsWithTag("Rotable"));

    }
    private float calcularAnguloComplementario()
    {
        float anguloZ = transform.eulerAngles.z;

        float anguloCuantizado = anguloZ / 90;
        anguloCuantizado = Mathf.Round(anguloCuantizado);
        anguloCuantizado *= 90;

        return anguloCuantizado;

    }
    

    private void elimitarParentsDeRotables()
    {
        foreach (GameObject objeto in objetosRotables)
        {

            if (objeto.transform.parent != null)
            {
                objeto.transform.parent = null;
            }


        }

        foreach (GameObject objeto in esquinas)
        {
            if (objeto != null)
            {
                if (objeto.transform.parent != null)
                {
                    objeto.transform.parent = null;
                }
            }
        }
    }
    public void agregarCirculoEnteroAlTransform()
    {
        foreach (GameObject objeto in objetosRotables)
        {
            if (objeto != null)
            {
                if (Vector2.Distance(objeto.transform.position, this.transform.position) < 1.76f)
                {
                    objeto.transform.parent = this.transform;
                }
            }
        }
        foreach (GameObject objeto in esquinas)
        {
            if (objeto != null)
            {
                if (Vector2.Distance(objeto.transform.position, this.transform.position) < 1.76f)
                {
                    objeto.transform.parent = this.transform;
                }
            }
        }

    }

    public void agregarCirculoPequeñoAlTRansform()
    {
        foreach (GameObject objeto in objetosRotables)
        {
            if (objeto != null)
            {
                if (Vector2.Distance(objeto.transform.position, this.transform.position) < 1.26f)
                {
                    objeto.transform.parent = this.transform;
                }
            }
        }
    }
    //  P & Y

    public IEnumerator girarCirculo()
    {
        float rotacionZactual = this.transform.eulerAngles.z;
        float contador = 0;
        rotando = true;
        while (contador < 90)
        {

            float calculoSuavizado = animationRotation.Evaluate(contador / 90);
            transform.eulerAngles = new Vector3(0, 0, rotacionZactual + 90 * calculoSuavizado);
            contador += Time.deltaTime * 175;
            yield return null;
        }

        rotando = false;
        transform.eulerAngles = new Vector3(0, 0, rotacionZactual + 90);
        giroCompleto?.Invoke(this,this.gameObject);

    }



    /// <summary>
    /// Giro el circulo completo.
    /// </summary>
    /// <param name="anguloGiro">90 o 180, no se permite rotaciones negativas</param>
    /// <param name="velocidad">tiempo que necesita para rotar el circulo</param>
    /// <returns>Retornara null mientras este girando, hasta que termine de girar</returns>
    public IEnumerator girandoCirculo(float anguloGiro, float velocidad, int interioroexterior)
    {
        elimitarParentsDeRotables();
        if (interioroexterior == 0)
        {
            agregarCirculoEnteroAlTransform();
        }
        if (interioroexterior == 1)
        {
            agregarCirculoPequeñoAlTRansform();
        }

        float rotacionZactual = this.transform.eulerAngles.z;
        float contador = 0;
        /*while (contador < anguloGiro)
        {
            contador += Time.deltaTime * velocidad * 0.5f;
            transform.eulerAngles = new Vector3(0, 0, rotacionZactual - contador);
            yield return null;
        }*/
        this.transform.eulerAngles = new Vector3(0, 0, rotacionZactual - anguloGiro);
        yield return new WaitForSeconds(velocidad);
        elimitarParentsDeRotables();

        giroCompleto?.Invoke(this, this.gameObject);
    }



    IEnumerator completarGiro()
    {
        float angulocomplementario = calcularAnguloComplementario();
        rotando = true;
        while (Mathf.Round(transform.eulerAngles.z) != angulocomplementario)
        {
            float smoothangle = Mathf.SmoothDampAngle(transform.eulerAngles.z, angulocomplementario, ref velocitySmoothCompleteGiro, 0.1f);

            transform.rotation = Quaternion.AngleAxis(smoothangle, Vector3.forward);
            yield return null;
        }
        rotando = false;
        transform.eulerAngles = new Vector3(0, 0, angulocomplementario);


        giroCompleto?.Invoke(this, this.gameObject);

    }

}
