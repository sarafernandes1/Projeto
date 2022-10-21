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

    public Image imagem_tempo;

    float cooldownTime = 4;
    float nextFireTime = 0;

    void Start()
    {
        imagem_tempo.fillAmount = 0;

    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (qtd_mana.value > 0.5f) can_atack = true;
            else can_atack = false;

            if (inputController.GetFeiticoNumber() == 5 && can_atack)
            {
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
            }
        }
    }

    public void Ataque()
    {

        sistema_particulas.Play();
        qtd_mana.value -= 0.5f;
       
        nextFireTime = Time.time + cooldownTime;
    }
}
