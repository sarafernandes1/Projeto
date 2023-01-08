using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoBossController : MonoBehaviour
{
    Animator animator;
    Transform player;
    Slider vida;
    public ParticleSystem ataque_especial, portal_segundaronda;
    public Rigidbody particle_normal, boladefogo;

    bool normal=true, procura=false, combate, segunda_ronda;
    float  distance;
    float speed = 2.0f;

    int numero = 0;
    float nextAtaqueEspecial = 0, cooldownAtaqueEspecial = 8, cooldownAtaqueSegundaRonda = 10;
    float nextAtaqueNormal = 0, cooldownAtaqueNormal = 3, nextFireAtaqueSR = 2f;
    float distanceToPlayer;

    public AudioSource andar, ataque;
    void Start()
    {
        animator = GetComponent<Animator>();
        vida = GameObject.Find("VidaBoss").GetComponent<Slider>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
         distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= 50.0f || BossHealth.Atingido)
        {
            procura = true;
        }

        if (normal)
        {
            animator.SetBool("idle_normal", true);
        }

        if (vida.value <= 0.5f)
        {
            segunda_ronda = true;
            speed = 3.5f;
        }

        if (procura)
        {
            andar.mute = false;
            transform.LookAt(player.position);
            Move();
            if(distance>=12.0f)transform.position += transform.forward * speed * Time.deltaTime;
            if (distanceToPlayer <= 60.0f && Time.time > nextFireAtaqueSR && procura && distanceToPlayer>=4)
            {
                combate = true;
                procura = false;
            }
            else
            {
                combate = false;
                procura = true;
            }
        }

        if (combate)
        {
            andar.mute = true;

            transform.LookAt(player.position);

            distance = Vector3.Distance(transform.position, player.position);
            if (distance > 30.0f)
            {
                animator.SetBool("attack_short_001", true);
                ataque.Play();
                AtaqueSegundaRonda();
            }

            if (distance <= 30.0f)
            {
                ataque.Play();
                animator.SetBool("attack_short_001", true);
                AtaqueNormal();
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
        yield return new WaitForSeconds(1.0f);
        animator.SetBool("attack_short_001", false);
        animator.SetBool("move_forward", true);
    }

    public void AtaqueNormal()
    {
        var bolaFogo = Instantiate(boladefogo, boladefogo.transform.position, Quaternion.identity);
        bolaFogo.gameObject.SetActive(true);
        bolaFogo.gameObject.GetComponent<ParticleSystem>().Play();
        bolaFogo.velocity = (player.position - boladefogo.transform.position).normalized * 30.0f;
        nextFireAtaqueSR = Time.time + cooldownAtaqueSegundaRonda;
        StartCoroutine(pararAtaque());

    }

    public void AtaqueSegundaRonda()
    {
        Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
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

   
}
