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

    void Start()
    {
        imagem_tempo.fillAmount = 0;
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (inputController.GetFeiticoNumber() == 0)
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
        nextFireTime = Time.time + cooldownTime;
    }


}
