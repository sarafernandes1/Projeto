using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFogoBoss : MonoBehaviour
{
    public ParticleSystem sistema_particulas, explosao;

    //AudioSource audio;
    bool explosion_active = false;
    public static float nextFireTime = 0;

    void Start()
    {
        //audio = GetComponent<AudioSource>();
       // audio.Play();
    }

    void Update()
    {
        if (sistema_particulas.isPlaying)
        {
            StartCoroutine(espera());
        }
    }

    void Ataque()
    {
        explosao.gameObject.SetActive(false);
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    IEnumerator waiter(ParticleSystem explosion)
    {
        //Wait for 2 seconds
        yield return new WaitForSeconds(1);

        Destroy(explosion.gameObject);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        sistema_particulas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        if (collision.gameObject.layer == 6) 
        {
            if (!explosion_active)
            {
                var explosion = Instantiate(explosao, transform.position, transform.rotation);
                explosion.gameObject.SetActive(true);
                explosion_active = true;
                explosion.Play();

                StartCoroutine(waiter(explosion));
            }
        }
    }
}
