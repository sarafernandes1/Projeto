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

    float cooldownTime = 2;
    float nextFireTime = 0;

    void Start()
    {

    }

    void Update()
    {
        //if (Time.time > nextFireTime)
        //{
        //    if (qtd_mana.value > 0.35f) can_atack = true;
        //    else can_atack = false;

        //    if (inputController.GetFeiticoNumber() == 2 && can_atack && LuzBastao.numero_feitico==-1
        //        || inputController.GetFeiticoNumber() == 2 && LuzBastao.numero_feitico == 5)
        //    {
        //        if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = 2;
        //        Ataque();
        //        cooldown = true;
        //    }
        //}

        //if (cooldown)
        //{
        //    imagem_tempo.fillAmount += 1 / cooldownTime * Time.deltaTime;

        //    if (imagem_tempo.fillAmount >= 1)
        //    {
        //        if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = -1;
        //        imagem_tempo.fillAmount = 0;
        //        cooldown = false;
        //    }
        //}

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

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == 7 || other.layer == 8)
        {
            sistema_particulas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    void Ataque()
    {
        nextFireTime = Time.time + cooldownTime;
    }
}
