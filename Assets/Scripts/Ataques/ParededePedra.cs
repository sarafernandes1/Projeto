using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParededePedra : MonoBehaviour
{
    public GameObject parede, parede_ataque, explosao, player;
    public InputController inputController;
    public AudioSource audio;
    public Slider qtd_mana;
    public Image imagem_tempo;
    public Rigidbody rigidbody;

    public static float cooldownTime = 4;
    
    float nextFireTime = 0;
    bool can_atack, cooldown;
    bool parede_ativa = false;

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

            if (inputController.GetFeiticoNumber() == 3 && !parede_ativa && qtd_mana.value >= 0.4f && LuzBastao.numero_feitico == -1
                    || inputController.GetFeiticoNumber() == 3 && !parede_ativa && qtd_mana.value >= 0.4f && LuzBastao.numero_feitico == 5)
            {
                rigidbody.isKinematic = false;
                if(LuzBastao.numero_feitico!=5) LuzBastao.numero_feitico = 3;
                AtivarParede();
                if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = -1;
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

    IEnumerator espera()
    {
        yield return new WaitForSeconds(4);
        rigidbody.isKinematic = true;
        parar();
    }

    IEnumerator parar()
    {
        yield return new WaitForSeconds(5);
        explosao.SetActive(false);
    }

    void AtivarParede()
    {
        parede_ativa = true;
        qtd_mana.value -= 0.4f;
        parede.SetActive(true);
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        float spawnDistance = 4.5f;
        Vector3 posicao_parede = playerPosition + playerDirection * spawnDistance;
        transform.rotation = playerRotation;
        transform.position =new Vector3( posicao_parede.x,posicao_parede.y, posicao_parede.z);
        explosao.SetActive(true);
        audio.Play();
        StartCoroutine(espera());
    }
}
