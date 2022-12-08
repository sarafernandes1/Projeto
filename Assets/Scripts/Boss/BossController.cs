using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public GameObject player;
    public GameObject[] pocisionamento_inimigo;
    public Rigidbody enemy, enemy2, ataque_especial;
    public Rigidbody enemy_alcance;
    public Canvas qtd_vida;
    public Canvas canvas;
    public ParticleSystem particle1;

    Vector3 position;
    float distanceToPlayer;
    int n_inimigos, p_numero=0;
    float cooldownTime = 10, nextenemy = 0;
    float random;

    void Start()
    {
    }

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        GameObject parede = GameObject.Find("ParedeCave");
        Collider collider = parede.transform.GetComponent<Collider>();

        if (distanceToPlayer<=90.0f)
        {
            qtd_vida.enabled = true;
            collider.enabled = true;
            if (Time.time > nextenemy && n_inimigos <= 8)
            {
                PosicionarInimigo();
                p_numero++;
                if (p_numero >= 5) p_numero = 1;
            }
        }
    }


    void PosicionarInimigo()
    {
        n_inimigos += 2;

        Vector3 posicao = pocisionamento_inimigo[p_numero].transform.position;
        Quaternion rotacao = pocisionamento_inimigo[p_numero].transform.rotation;
        random = Random.Range(-10,10);
        if (random <= 0f)
        {
            var inimigo_chamdo1 = Instantiate(enemy, posicao, rotacao);
           // inimigo_chamdo1.velocity = transform.forward * 20;
        }
        else
        {
            var inimigo_chamdo1 = Instantiate(enemy, posicao, rotacao);
        }
        // var inimigo_chamdo2 = Instantiate(enemy_alcance, posicao,rotacao);
        // inimigo_chamdo2.velocity = transform.forward * 30;
        nextenemy = Time.time + cooldownTime;
    }
}
