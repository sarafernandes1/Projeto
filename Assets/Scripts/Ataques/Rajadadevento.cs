using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rajadadevento : MonoBehaviour
{
    public InputController inputController;
    public ParticleSystem sistema_particulas;
    public Slider qtd_mana;
    bool can_atack, cooldown;

    public SphereCollider collider_particle;

    public static float mana_necessario = 0.45f;

    public Image imagem_tempo;

    float cooldownTime = 2;
    float nextFireTime = 0, timer=0;

    void Start()
    {
        imagem_tempo.fillAmount = 0;
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (qtd_mana.value > 0.45f) can_atack = true;
            else can_atack = false;

            if (inputController.GetFeiticoNumber() == 4 && can_atack)
            {
                Ataque();
                cooldown = true;
            }
        }

        float distance = Vector3.Distance(sistema_particulas.transform.position, collider_particle.transform.position);
        if (Time.time > timer)
        {
            collider_particle.enabled = false;
        }

        if (cooldown)
        {
            imagem_tempo.fillAmount += 1 / cooldownTime * Time.deltaTime;
            if (imagem_tempo.fillAmount >= 1)
            {
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
        qtd_mana.value -= mana_necessario;
        collider_particle.enabled = true;
        nextFireTime = Time.time + cooldownTime;
        timer = Time.time + 0.05f;
    }

}
