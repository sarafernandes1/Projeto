using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarBoss : MonoBehaviour
{
    Animator animator;
    Transform player;
    bool normal, procura, ataque;
    float distanceToPlayer, distance;

    void Start()
    {
        animator = GetComponent<Animator>();
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

            transform.position += transform.forward * 2.0f * Time.deltaTime;
            distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= 25.0f)
            {
                procura = false;
                ataque = true;
                IdleCombat();
            }
        }

        if (ataque)
        {
            distance= Vector3.Distance(transform.position, player.position);
            if (distance <= 20.0f)
            {
                animator.SetBool("attack_short_001", true);
            }
        }

        //Ataque Normal-> particula
        //Ataque Especial-> zonas da cave marcadas e são lançadas bolas de fogo

        if (BossHealth.isdead)
        {
            animator.SetBool("dead", true);
        }
    }

    public void Move()
    {
        animator.SetBool("idle_normal", false);
        animator.SetBool("move_forward", true);
    }

    public void IdleCombat()
    {
        animator.SetBool("idle_combat", true);
        animator.SetBool("move_forward", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !ataque)
        {
            procura = true;
        }
    }
}
