using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // xot is called before the first frame update
    private float longitud, xo;
    public GameObject cam;
    public float parallaxEffect;

    void xot()
    {
        // Posición inicial del fondo.
        xo = transform.position.x;

        // Longitud del fondo.
        longitud = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
    
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(xo + dist, transform.position.y, transform.position.z);

        if (temp > xo + longitud)
        {
            xo += longitud;
        }
        else if (temp < xo - longitud)
        {
            xo -= longitud;
        }
    }
}
