using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzEfeito : MonoBehaviour
{
    Light luz;
    bool crescer, decrescer=true;
    float intensidade_inicial;

    void Start()
    {
        luz = GetComponent<Light>();
        intensidade_inicial = luz.intensity;
    }


    void Update()
    {
        if (luz.intensity <= 0.3f)
        {
            decrescer = false;
            crescer = true;
        }

        if (luz.intensity >= intensidade_inicial)
        {
            decrescer = true;
            crescer = false;
        }

        if (decrescer)
        {
            luz.intensity -= 0.2f * Time.deltaTime;
        }

        if (crescer)
        {
            luz.intensity += 0.2f *Time.deltaTime;
        }
    }
}
