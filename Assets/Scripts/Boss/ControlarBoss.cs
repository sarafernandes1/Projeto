using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlarBoss : MonoBehaviour
{
    Animator animator;
    Transform player;
    Slider vida;
    public ParticleSystem ataque_especial, portal_segundaronda;
    public Rigidbody particle_normal, boladefogo;

    bool normal, procura, combate, ataque;
    float distanceToPlayer, distance;
    float speed = 2.0f;

    float nextAtaqueEspecial = 0, cooldownAtaqueEspecial = 8, cooldownAtaqueSegundaRonda=10;
    float nextAtaqueNormal = 0, cooldownAtaqueNormal = 3, nextFireAtaqueSR=0;


    void Start()
    {
        animator = GetComponent<Animator>();
        vida = GameObject.Find("VidaBoss").GetComponent<Slider>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (normal)
        {
            animator.SetBool("idle_normal", true);
        }

        //Segunda Ronda
        if (procura)
        {
            transform.LookAt(player.position);
            Move();

            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer >= 30.0f)
            {
                animator.SetBool("move_forward", false);
                animator.SetBool("move_forward_fast", true);
                speed = 4.5f;
            }
            else
            {
                animator.SetBool("move_forward", true);
                animator.SetBool("move_forward_fast", false);
                speed = 2.0f;
            }
            transform.position += transform.forward * speed * Time.deltaTime;

                if (Time.time > nextFireAtaqueSR)
                {
                    animator.SetBool("attack_short_001", true);
                    animator.SetBool("idle_combat", false);

                    AtaqueSegundaRonda();
                }
            
        }

        //Passagem da primeira ronda para a segunda ronda
        if (combate && vida.value < 0.5f)
        {
            procura = true;
            combate = false;
            animator.SetBool("idle_normal", true);
            animator.SetBool("idle_combat", false);
        }

        //Primeira ronda
        if (combate)
        {
            IdleCombat();
            transform.LookAt(player.position);

            distance = Vector3.Distance(transform.position, player.position);
            if (distance <= 30.0f)
            {
                if (Time.time > nextAtaqueNormal)
                {
                    animator.SetBool("attack_short_001", true);
                    AtaqueNormal();
                }
                else
                {
                    animator.SetBool("attack_short_001", false);
                }
            }
            else
            {
                animator.SetBool("attack_short_001", false);
            }

            if (Time.time > nextAtaqueEspecial)
            {
                AtaqueEspecial();
            }
        }

        if (BossHealth.isdead)
        {
            procura = false;
            animator.SetBool("dead", true);
        }
    }

    IEnumerator destruirParticula(GameObject p)
    {
        yield return new WaitForSeconds(6);
        Destroy(p.gameObject);
    }

    IEnumerator pararAtaque()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("attack_short_001", false);
        animator.SetBool("move_forward", true);
    }

    public void AtaqueNormal()
    {
        var bolaFogo = Instantiate(boladefogo, boladefogo.transform.position, Quaternion.identity);
        bolaFogo.gameObject.SetActive(true);
        bolaFogo.gameObject.GetComponent<ParticleSystem>().Play();
        bolaFogo.velocity = (player.position - boladefogo.transform.position).normalized * 30.0f;
        nextAtaqueNormal = Time.time + cooldownAtaqueNormal;
    }

    public void AtaqueEspecial()
    {
        Vector3 position_particle1 = ataque_especial.transform.position;
        Vector3 position_particle2 = new Vector3(Random.Range(-80, -63), 3.0f, Random.Range(11, 57));

        var particle1 = Instantiate(ataque_especial, position_particle1, Quaternion.identity);
        particle1.gameObject.transform.Rotate(90,0,0);
        particle1.gameObject.SetActive(true);
        particle1.Play();
        var particle2 = Instantiate(ataque_especial, position_particle2, Quaternion.identity);
        particle2.gameObject.transform.Rotate(90, 0, 0);
        particle2.gameObject.SetActive(true);
        particle2.Play();

        nextAtaqueEspecial = Time.time + cooldownAtaqueEspecial;

        StartCoroutine(destruirParticula(particle1.gameObject));
        StartCoroutine(destruirParticula(particle2.gameObject));
    }

    public void AtaqueSegundaRonda()
    {
        Vector3 position = new Vector3(player.transform.position.x,player.transform.position.y, player.transform.position.z) ;
        Quaternion rotation = portal_segundaronda.transform.rotation;
        var portal = Instantiate(portal_segundaronda, position, rotation);
        portal.gameObject.SetActive(true);
        portal.Play();

        nextFireAtaqueSR = Time.time + cooldownAtaqueSegundaRonda;

        StartCoroutine(pararAtaque());
        StartCoroutine(destruirParticula(portal.gameObject));
    }

    public void Move()
    {
        animator.SetBool("idle_normal", false);
        animator.SetBool("idle_combat", false);
        animator.SetBool("move_forward", true);
    }

    public void IdleCombat()
    {
        animator.SetBool("idle_combat", true);
        animator.SetBool("move_forward", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !procura)
        {
            combate = true;
        }
    }
}
