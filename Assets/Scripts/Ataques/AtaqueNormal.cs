using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtaqueNormal : MonoBehaviour
{
    public InputController inputController;
    public ParticleSystem sistema_particulas;

    AudioSource audio;

    public static bool ataque;

    float cooldownTime = 0.8f;
    float nextFireTime = 0;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
    }

    void Update()
    {
        if (ataque)
        {
            if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = 0;
            Ataque();
        }

        if (sistema_particulas.isPlaying)
        {
            StartCoroutine(espera());
        }
    }

    void Ataque()
    {
        nextFireTime = Time.time + cooldownTime;
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == 3 || other.layer == 8 || other.layer == 10)
        {
            sistema_particulas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        else Destroy(this.gameObject);
    }

}
