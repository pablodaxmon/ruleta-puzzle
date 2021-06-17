using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    private bool showing;
    int rnd;
    public void Start()
    {
        StartCoroutine(showCircle());
    }
    public void Update()
    {
        if (!showing)
        {
            showing = true;
            StartCoroutine(showCircle());
        }
    }

    private IEnumerator showCircle()
    {

        rnd = Random.Range(0, 20);
        yield return new WaitForSeconds(rnd);
        while (transform.localScale.x < 1)
        {
            transform.localScale += new Vector3(Time.deltaTime,Time.deltaTime,1);
            yield return null;
        }
        transform.localScale = Vector3.one;
        yield return new WaitForSeconds(2);
        rnd = Random.Range(0, 8);
        yield return new WaitForSeconds(rnd);

        while (transform.localScale.x > 0.001f)
        {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, 1);
            yield return null;
        }
        transform.localScale = Vector3.zero;
        showing = false;
    }
}
