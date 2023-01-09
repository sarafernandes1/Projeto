using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaioEletrico : MonoBehaviour
{
    public InputController inputController;
    public ParticleSystem sistema_particulas;

    public static float mana_gasto=0.35f;
    public static bool ataque;

    AudioSource audio;

    float cooldownTime = 2;
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
            if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = 2;
            Ataque();
        }

        if (sistema_particulas.isPlaying)
        {
            StartCoroutine(espera());
        }
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer==10) Destroy(this.gameObject);
    }

        private void OnParticleCollision(GameObject other)
    {
        if (other.layer == 3 || other.layer == 8 || other.layer == 8) sistema_particulas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        else if(other.gameObject.layer != 10) Destroy(this.gameObject);
    }

    void Ataque()
    {
        nextFireTime = Time.time + cooldownTime;
    }
}
