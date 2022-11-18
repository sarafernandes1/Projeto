using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuriaAncestral : MonoBehaviour
{
    public Slider qtd_mana;
    public InputController inputController;
    public ParticleSystem sistema_particulas;
    bool can_atack, cooldown;

    AudioSource audio;

    public Image imagem_tempo;

    public static float cooldownTime = 4;
    float nextFireTime = 0;

    void Start()
    {
        imagem_tempo.fillAmount = 0;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (qtd_mana.value > 0.5f) can_atack = true;
            else can_atack = false;

            if (inputController.GetFeiticoNumber() == 5 && can_atack)
            {
                LuzBastao.numero_feitico = 5;
                Ataque();
                cooldown = true;
            }
        }

        if (cooldown)
        {
            imagem_tempo.fillAmount += 1 / cooldownTime * Time.deltaTime;
            if(qtd_mana.value<1) qtd_mana.value += 0.6f * Time.deltaTime;
            if (imagem_tempo.fillAmount >= 1)
            {
                imagem_tempo.fillAmount = 0;
                cooldown = false;
                LuzBastao.numero_feitico = -2;
            }
        }
    }

    public void Ataque()
    {
        sistema_particulas.Play();
        qtd_mana.value -= 0.5f;
        audio.Play();
        nextFireTime = Time.time + cooldownTime;
    }
}
