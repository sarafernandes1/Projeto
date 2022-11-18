using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoladeFogo : MonoBehaviour
{
    public InputController inputController;
    public ParticleSystem sistema_particulas, explosao;

    public AudioSource audio_bolafogo, audio_explosao;

    public static bool ataque;

    float cooldownTime = 2;
    bool explosion_active = false;
    public static float nextFireTime = 0;

    void Start()
    {
        audio_bolafogo.Play();
    }

    void Update()
    {
        if (ataque)
        {
            if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = 1;
            Ataque();
        }

        if (sistema_particulas.isPlaying)
        {
            StartCoroutine(espera());
        }
    }

    void Ataque()
    {
        explosao.gameObject.SetActive(false);
        nextFireTime = Time.time + cooldownTime;
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

        if (collision.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (!explosion_active)
            {
                var explosion = Instantiate(explosao, transform.position, transform.rotation);
                explosion.gameObject.SetActive(true);
                explosion_active = true;
                explosion.Play();
                audio_explosao.Play();
                StartCoroutine(waiter(explosion));
            }
        }
    }

}
