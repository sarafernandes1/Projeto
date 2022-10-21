using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParededePedra : MonoBehaviour
{
    public GameObject parede, parede_ataque;
    public GameObject player;
    public InputController inputController;
    bool parede_ativa = false;
    public Slider qtd_mana;
    bool can_atack, cooldown;
    public Image imagem_tempo;

    float cooldownTime = 4;
    float nextFireTime = 0;

    void Start()
    {
        imagem_tempo.fillAmount = 0;
        parede_ataque = parede;
    }

    void Update()
    {
        
        if (Time.time > nextFireTime)
        {
            if (qtd_mana.value > 0.4f) can_atack = true;
            else can_atack = false;

            if (inputController.GetFeiticoNumber() == 3 && !parede_ativa && qtd_mana.value >= 0.4f)
            {
                AtivarParede();
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
                can_atack = true;
                parede_ativa = false;
                parede_ataque.SetActive(false);
            }
        }
       

    }

    void AtivarParede()
    {
        parede_ativa = true;
        qtd_mana.value -= 0.4f;
        parede.SetActive(true);

        Vector3 playerPosition = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        float spawnDistance = 0.7f;
        Vector3 posicao_parede = playerPosition + playerDirection * spawnDistance;
        transform.rotation = playerRotation;
        transform.position = posicao_parede;

        nextFireTime = Time.time + cooldownTime;
        
    }
}
