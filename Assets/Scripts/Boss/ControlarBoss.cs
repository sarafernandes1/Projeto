using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlarBoss : MonoBehaviour
{
    Animator animator;
    Transform player;
    Slider vida;
    public ParticleSystem particle, portal_segundaronda;
    public Rigidbody particle_normal, boladefogo;

    bool normal, procura, ataque;
    float distanceToPlayer, distance;
    float speed = 2.0f;

    float nextAtaqueEspecial = 0, cooldownAtaqueEspecial = 7, cooldownAtaqueSegundaRonda=9;
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

        if (procura)
        {
            transform.LookAt(player.position);
            Move();

            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer >= 30.0f)
            {
                animator.SetBool("move_forward", false);
                animator.SetBool("move_forward_fast", true);
                speed = 4.0f;
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

        if (ataque && vida.value < 0.5f)
        {
            procura = true;
            ataque = false;
            animator.SetBool("idle_normal", true);
            animator.SetBool("idle_combat", false);
        }

        if (ataque)
        {
            IdleCombat();
            transform.LookAt(player.position);

            distance = Vector3.Distance(transform.position, player.position);
            if (distance <= 20.0f)
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
        yield return new WaitForSeconds(2);
        animator.SetBool("attack_short_001", false);
        animator.SetBool("move_forward", true);
    }

    public void AtaqueNormal()
    {
        particle_normal.gameObject.SetActive(true);
        particle_normal.gameObject.GetComponent<ParticleSystem>().Play();
        var bolaFogo = Instantiate(boladefogo, boladefogo.transform.position, Quaternion.identity);
        bolaFogo.gameObject.SetActive(true);
        bolaFogo.gameObject.GetComponent<ParticleSystem>().Play();
        bolaFogo.velocity = (player.position - boladefogo.transform.position).normalized * 30.0f;
        nextAtaqueNormal = Time.time + cooldownAtaqueNormal;
    }

    public void AtaqueEspecial()
    {
        Vector3 position_particle1 = new Vector3(Random.Range(-60, -100), 8.0f, Random.Range(8, 25));
        Vector3 position_particle2 = new Vector3(Random.Range(-50, -110), 8.0f, Random.Range(50, 70));

        var particle1 = Instantiate(particle, position_particle1, particle.transform.rotation);
        particle1.gameObject.SetActive(true);
        particle1.Play();
        var particle2 = Instantiate(particle, position_particle2, particle.transform.rotation);
        particle2.gameObject.SetActive(true);
        particle2.Play();

        nextAtaqueEspecial = Time.time + cooldownAtaqueEspecial;

        StartCoroutine(destruirParticula(particle1.gameObject));
        StartCoroutine(destruirParticula(particle2.gameObject));
    }

    public void AtaqueSegundaRonda()
    {
        Vector3 position = portal_segundaronda.transform.position;
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
            ataque = true;
        }
    }
}
