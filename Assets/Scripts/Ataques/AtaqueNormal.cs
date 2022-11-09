using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtaqueNormal : MonoBehaviour
{
    public InputController inputController;
    public ParticleSystem sistema_particulas;
    bool cooldown;
    public Image imagem_tempo;

    float cooldownTime = 0.8f;
    float nextFireTime = 0;

    GameObject bastao;

    void Start()
    {
        imagem_tempo.fillAmount = 0;
        bastao = GameObject.Find("Bastão");
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (inputController.GetFeiticoNumber() == 0 && LuzBastao.numero_feitico==-1
                || inputController.GetFeiticoNumber()==0 && LuzBastao.numero_feitico==5)
            {
              if(LuzBastao.numero_feitico!=5 ) LuzBastao.numero_feitico = 0;
                Ataque();
                cooldown = true;
            }
        }

        if (cooldown)
        {
            imagem_tempo.fillAmount += 1 / cooldownTime * Time.deltaTime;
            
            if (imagem_tempo.fillAmount >= 1)
            {
                if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = -1;
                imagem_tempo.fillAmount = 0;
                cooldown = false;
            }
        }
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
        sistema_particulas.Play();
        nextFireTime = Time.time + cooldownTime;
    }


}
