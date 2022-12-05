using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzEfeito : MonoBehaviour
{
    Light luz;
    bool crescer, decrescer=true;

    void Start()
    {
        luz = GetComponent<Light>();
    }


    void Update()
    {
        if (luz.intensity <= 0)
        {
            decrescer = false;
            crescer = true;
        }

        if (luz.intensity >= 0.5f)
        {
            decrescer = true;
            crescer = false;
        }

        if (decrescer)
        {
            luz.intensity -= 0.1f * Time.deltaTime;
        }

        if (crescer)
        {
            luz.intensity += 0.1f *Time.deltaTime;
        }
    }
}
