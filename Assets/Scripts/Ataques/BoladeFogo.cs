using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoladeFogo : MonoBehaviour
{
    public InputController inputController;
    public ParticleSystem sistema_particulas, explosao;
    public Slider qtd_mana;
    public Image imagem_tempo;

    float cooldownTime = 2;
    float nextFireTime = 0;
    bool can_atack, cooldown;


    void Start()
    {
        imagem_tempo.fillAmount = 0;
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (qtd_mana.value > 0.3f) can_atack = true;
            else can_atack = false;

            if (inputController.GetFeiticoNumber() == 1 && can_atack)
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
        sistema_particulas.gameObject.SetActive(true);
        explosao.gameObject.SetActive(false);
        sistema_particulas.Play();
        qtd_mana.value -= 0.3f;

        nextFireTime = Time.time + cooldownTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        sistema_particulas.gameObject.SetActive(false);
        explosao.gameObject.SetActive(true);
        explosao.Play();
        explosao.transform.position = other.transform.position;
    }
}
