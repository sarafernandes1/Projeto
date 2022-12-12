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
    public Animator animator;
    AudioSource audio;

    public Image imagem_tempo;

    public static float cooldownTime = 4;
    float nextFireTime = 0;
    public Text tempo;
    float timer = 30;
    bool indisponivel=false, fim=false;

    void Start()
    {
        imagem_tempo.fillAmount = 0;
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time > nextFireTime && !MelhoriaFeiticos.gamePaused && !indisponivel)
        {
            if (qtd_mana.value > 0.5f) can_atack = true;
            else can_atack = false;

            if (inputController.GetFeiticoNumber() == 5 && can_atack)
            {
                animator.Play("Magia_Furia_Ancestral");
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
                fim = true;
            }
        }

        if (!cooldown && fim)
        {
            indisponivel = true;
            if (timer < 0)
            {
                indisponivel = false;
                tempo.text = "";
                fim = false;
                timer = 30;
            }
            if (indisponivel)
            {
                int t = (int)timer;
                tempo.text =t.ToString();
                timer -= 1*Time.deltaTime;
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
