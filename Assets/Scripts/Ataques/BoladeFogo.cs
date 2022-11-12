using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoladeFogo : MonoBehaviour
{
    public InputController inputController;
    public ParticleSystem sistema_particulas, explosao;

    public static bool ataque;

    float cooldownTime = 2;
    public static float nextFireTime = 0;

    void Start()
    {
    }

    void Update()
    {
        if (ataque)
        {
            if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = 1;
            Ataque();
        }
    }

    void Ataque()
    {
        explosao.gameObject.SetActive(false);
        sistema_particulas.gameObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,0,10));
        nextFireTime = Time.time + cooldownTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //explosao.gameObject.SetActive(true);
        explosao.transform.position = collision.gameObject.transform.position;

        explosao.Play();
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8)
        {
            sistema_particulas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    //private void OnParticleCollision(GameObject other)
    //{
    //    //sistema_particulas.gameObject.SetActive(false);
    //    explosao.gameObject.SetActive(true);
    //    explosao.Play();
    //    explosao.transform.position = other.transform.position;

    //    if (other.layer == 7 || other.layer == 8)
    //    {
    //        sistema_particulas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    //    }
    //}

    
}
