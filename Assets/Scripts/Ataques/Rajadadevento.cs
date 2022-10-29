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

    public static float mana_necessario = 0.45f;

    public Image imagem_tempo;

    float cooldownTime = 2;
    float nextFireTime = 0;

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

    void Ataque()
    {
        sistema_particulas.Play();
        qtd_mana.value -= mana_necessario;

        nextFireTime = Time.time + cooldownTime;
    }
}
