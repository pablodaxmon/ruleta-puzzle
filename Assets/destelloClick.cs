using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destelloClick : MonoBehaviour
{
    private SpriteRenderer spritecolor;

    [Header("Fading circle")]
    public AnimationCurve fading;

    [Header("Time duration effect")]
    public float speedEffect = 1;
    public void Start()
    {
        spritecolor = GetComponent<SpriteRenderer>();
        StartCoroutine(effectCoroutine());
    }

    
    private IEnumerator effectCoroutine()
    {
        Color colorOrigin = spritecolor.color;
        float time = 0;
        while(time < 1)
        {
            time += Time.deltaTime * speedEffect;

            spritecolor.color = new Color(colorOrigin.r, colorOrigin.g, colorOrigin.b, fading.Evaluate(time));
            gameObject.transform.localScale += Vector3.one* Time.deltaTime * speedEffect;
            yield return null;

        }

        Object.Destroy(this.gameObject);
    }
}
