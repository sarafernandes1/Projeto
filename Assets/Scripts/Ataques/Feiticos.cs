using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feiticos : MonoBehaviour
{
    public InputController inputController;
    public Rigidbody bolafogo;

    public Slider qtd_mana;
    public Image imagem_tempo;

    float cooldownTime = 2;
    public static float nextFireTime = 0;
    bool can_atack, cooldown;

    void Start()
    {
        imagem_tempo.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            PodesAtacar(0.3f);

            if (inputController.GetFeiticoNumber() == 1 && can_atack && LuzBastao.numero_feitico == -1
                || inputController.GetFeiticoNumber() == 1 && LuzBastao.numero_feitico == 5)
            {
                var projectile = Instantiate(bolafogo, bolafogo.gameObject.transform.position, bolafogo.gameObject.transform.rotation);
                projectile.gameObject.SetActive(true);
                projectile.AddForce(transform.forward * 100);
                projectile.gameObject.GetComponent<ParticleSystem>().Play();
                nextFireTime = Time.time + cooldownTime;
                cooldown = true;
                qtd_mana.value -= 0.3f;
                BoladeFogo.ataque = true;
            }

        }

        if (cooldown)
        {
            BoladeFogo.ataque = ImagemCooldown();
        }
    }

    public void PodesAtacar(float mana_necessaria)
    {
        if (qtd_mana.value > mana_necessaria) can_atack = true;
        else can_atack = false;
    }

    public bool ImagemCooldown()
    {
        imagem_tempo.fillAmount += 1 / cooldownTime * Time.deltaTime;

        if (imagem_tempo.fillAmount >= 1)
        {
            if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = -1;
            imagem_tempo.fillAmount = 0;
            cooldown = false;
            return false;
        }
        else
        {
            return true;
        }
    }
  
}
