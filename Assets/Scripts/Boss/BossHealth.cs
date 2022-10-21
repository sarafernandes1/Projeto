using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider qtd_vida;
    bool isdead;

    void Start()
    {
        
    }

    void Update()
    {
        if (qtd_vida.value <= 0)
        {
            isdead = true;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "AtaqueNormal")
        {
            qtd_vida.value -= 0.01f;
        }
        if (other.gameObject.name == "Boladefogo")
        {
            qtd_vida.value -= 0.01f;
            other.SetActive(true);
        }
        if (other.gameObject.name == "RaioEletrico")
        {
            qtd_vida.value -= 0.04f;
            other.SetActive(true);
        }

        if (Vector3.Distance(transform.position, other.transform.position) <= 20.0f && other.name == "Rajadadevento")
        {
            transform.position -= transform.forward * 2.0f * 2.0f;
            qtd_vida.value -= 0.03f;
        }

    }


}
