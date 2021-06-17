using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarGeneratorUI : MonoBehaviour
{
    private bool showing;
    int rnd;
    private Image image;
    private Color starcolor;
    public void Start()
    {
        StartCoroutine(showCircle());
        image = GetComponent<Image>();
        starcolor = new Color( image.color.r, image.color.g, image.color.b, 1);
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
        while (image.color.a < 1)
        {
            image.color += new Color(0,0,0, Time.deltaTime);
            yield return null;
        }
        image.color = starcolor;

        yield return new WaitForSeconds(4);

        while (image.color.a > 0.001f)
        {
            image.color -= new Color(0, 0, 0, Time.deltaTime);
            yield return null;
        }
        image.color = new Color(starcolor.r, starcolor.g, starcolor.b, 0);
        showing = false;
    }
}
