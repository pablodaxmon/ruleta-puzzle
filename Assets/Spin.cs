using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float velocidad = 1;
    private Vector3 vectorVelocidad;
    private void Start()
    {
        vectorVelocidad = new Vector3(0,0,velocidad *0.01f);
    }
    void Update()
    {
        transform.Rotate(vectorVelocidad);        
    }
}
