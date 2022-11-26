using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider qtd_vida;
    public static bool isdead;
    public Animator animator;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "AtaqueNormal(Clone)")
        {
            qtd_vida.value -= 0.1f;
            //qtd_vida.value -= 0.3f*Time.deltaTime
        }
        if (collision.gameObject.name == "Boladefogo(Clone)")
        {
            qtd_vida.value -= 0.4f * Time.deltaTime;

        }
        if (collision.gameObject.name == "RaioEletrico(Clone)")
        {
            qtd_vida.value -= 0.4f * Time.deltaTime;
        }
    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "Rajadadevento")
        {
            qtd_vida.value -= 0.05f*Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Explosao(Clone)")
        {
            qtd_vida.value -= 0.3f * Time.deltaTime;
        }
    }


}
