using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentoEfeito : MonoBehaviour
{
    float timer = 0, tempo_rajada = 0.10f;
    public bool rajada_on;
    float forca = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (rajada_on)
        {
            transform.position -= transform.forward * Time.deltaTime * forca;
            if (Time.time > timer)
            {
                rajada_on = false;
            };
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Rajadadevento" && !rajada_on)
        {
            rajada_on = true;
            timer = Time.time + tempo_rajada;
        }
    }
}
