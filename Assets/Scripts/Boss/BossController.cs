using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] pocisionamento_inimigo;
    public Rigidbody enemy, ataque_especial;
    public GameObject enemy_alcance;
    public Collider ataque_collider;
    public Slider qtd_vida;
    public ParticleSystem particle1;

    bool segunda_parte;
    float distanceToPlayer;
    int n_inimigos, p_numero=0;
    float cooldownTime = 10, cooldownAtaque = 6, cooldownAE=6;
    float nextFireTime = 0, nextenemy = 0, nexTimetAE=3;

    void Start()
    {
        
    }

    void Update()
    {
        //Vector3 lookAt = player.transform.position;
        //lookAt.y = transform.position.y;
        //transform.LookAt(lookAt);

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        GameObject parede = GameObject.Find("ParedeCave");
        Collider collider = parede.transform.GetComponent<Collider>();
        //if(!GameObject.Find("Cylinder (61)") && !GameObject.Find("Cylinder (62)") && !GameObject.Find("Cylinder (63)"))
        //{
        //    collider.enabled = false;
        //}
        //else
        //{
        //    collider.enabled = true;
        //}

        if (distanceToPlayer<=80.0f)
        {
            qtd_vida.gameObject.SetActive(true);
           // collider.enabled = true;
            if (Time.time > nextenemy && n_inimigos <= 4)
            {
                PosicionarInimigo();
                p_numero++;
                if (p_numero >= 5) p_numero = 1;
            }
        }
        
        //if (distanceToPlayer <= 60.0f)
        //{
        //    if (Time.time > nextFireTime)
        //    {
        //        Ataque();
               
        //    }
        //    if (Time.time > nexTimetAE)
        //    {
        //        AtaqueEspecial();
        //    }
        //}

        //if (particle1.isEmitting == false) ataque_collider.enabled = false;

        //if (qtd_vida.value<=0.5f && collider.enabled)
        //{
        //    transform.position += transform.forward * 1.0f * Time.deltaTime;
        //    segunda_parte = true;
        //}

    }

    void Ataque()
    {
        particle1.Play();
        ataque_collider.enabled = true;
        nextFireTime = Time.time + cooldownAtaque;
    }

    void AtaqueEspecial()
    {
        transform.LookAt(player.transform.position);

        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        var projectile = Instantiate(ataque_especial, position,transform.rotation);
        projectile.velocity =transform.forward* 80;
      
        nexTimetAE = Time.time + cooldownAE;
    }

    void PosicionarInimigo()
    {
        n_inimigos += 2;

        Vector3 posicao = pocisionamento_inimigo[p_numero].transform.position;
        Quaternion rotacao = pocisionamento_inimigo[p_numero].transform.rotation;
        var inimigo_chamdo1 = Instantiate(enemy, posicao,rotacao);
        var inimigo_chamdo2 = Instantiate(enemy_alcance, posicao,rotacao);
        inimigo_chamdo1.velocity = transform.forward * 20;

        nextenemy = Time.time + cooldownTime;
    }
}
