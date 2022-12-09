using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParededePedra : MonoBehaviour
{
    public GameObject parede, parede_ataque, explosao, player, rocha;
    public InputController inputController;
    public AudioSource audio;
    public Slider qtd_mana;
    public Image imagem_tempo;
    public Rigidbody rigidbody;

    public static GameObject objeto;
    public static float cooldownTime = 4;
    
    float nextFireTime = 0;
    bool can_atack, cooldown;
    bool parede_ativa = false;
    public GameObject p;
    public Animator animator;

    void Start()
    {
        imagem_tempo.fillAmount = 0;
        parede_ataque = parede;
        objeto = parede;
        if (MelhoriaFeiticos.mudanca_parede && p==null)
        {
            objeto = rocha;
        }

        if (GuardarInformacao.tempo_cooldownparede != 4)
            cooldownTime = GuardarInformacao.tempo_cooldownparede;
    }

    void Update()
    {
        
        if (Time.time > nextFireTime && !MelhoriaFeiticos.gamePaused) 
        {
            if (qtd_mana.value > 0.4f) can_atack = true;
            else can_atack = false;

            if (inputController.GetFeiticoNumber() == 3 && !parede_ativa && qtd_mana.value >= 0.4f && LuzBastao.numero_feitico == -1
                    || inputController.GetFeiticoNumber() == 3 && !parede_ativa && qtd_mana.value >= 0.4f && LuzBastao.numero_feitico == 5)
            {
                animator.Play("Magia_Parede_de_Pedra");
                //rigidbody.isKinematic = false;
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
                objeto.SetActive(false);
            }
        }
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(0.5f);
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

        StartCoroutine(espera());
        parede_ativa = true;
        qtd_mana.value -= 0.4f;
        objeto.SetActive(true);
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        float spawnDistance = 4.5f;
        Vector3 posicao_parede = playerPosition + playerDirection * spawnDistance;
        transform.rotation = playerRotation;
        transform.position =new Vector3( posicao_parede.x,posicao_parede.y, posicao_parede.z);
        explosao.SetActive(true);
        audio.Play();
    }
}
