using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] poscionamento_inimigo;
    public Rigidbody enemy, ataque_especial;
    public Collider ataque_collider;
    public Slider qtd_vida;
    public ParticleSystem particle1;
    bool segunda_parte;
    float speed = 1.0f;
    int n_inimigos, p_numero=1;
    float cooldownTime = 10, cooldownAtaque = 6, cooldownAE=10;
    float nextFireTime = 0, nextenemy = 0, nexTimetAE=4;

    void Start()
    {
        
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        transform.LookAt(player.transform.position);
        if (distanceToPlayer <= 40.0f)
        {
            if (Time.time > nextFireTime)
            {
                Ataque();
               
            }
            if (Time.time > nexTimetAE)
            {
                AtaqueEspecial();
            }
        }

        if (particle1.isEmitting == false) ataque_collider.enabled = false;


        if (distanceToPlayer <= 80.0f)
        {
            if (Time.time > nextenemy && n_inimigos <= 4)
            {
                PosicionarInimigo();
                p_numero++;
                if (p_numero >= 5) p_numero = 1;
            }
        }


        GameObject inimigos = GameObject.Find("InimigoBoss(Clone)");
        if (inimigos==null){

            //transform.position += transform.forward * speed * Time.deltaTime;
            segunda_parte = true;
        }

        if (distanceToPlayer <= 70.0f)
        {
            qtd_vida.gameObject.SetActive(true);
        }
        else
        {
            qtd_vida.gameObject.SetActive(false);
        }

    }

    void Ataque()
    {
        particle1.Play();
        ataque_collider.enabled = true;
        nextFireTime = Time.time + cooldownAtaque;
    }

    void AtaqueEspecial()
    {
        Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        var projectile = Instantiate(ataque_especial, position,transform.rotation);
        projectile.velocity =transform.forward* 80;

        nexTimetAE = Time.time + cooldownAE;
    }

    void PosicionarInimigo()
    {
        var inimigo_chamdo1 = Instantiate(enemy, poscionamento_inimigo[p_numero].transform.position, poscionamento_inimigo[p_numero].transform.rotation);
        var inimigo_chamdo2 = Instantiate(enemy, poscionamento_inimigo[p_numero].transform.position, poscionamento_inimigo[p_numero].transform.rotation);
        n_inimigos += 2;
        inimigo_chamdo1.velocity = transform.forward * 20;
        inimigo_chamdo2.velocity = transform.forward * 30;
        nextenemy = Time.time + cooldownTime;
    }
}
