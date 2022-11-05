using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    bool ataque_alcance, ataque_boss, segunda_parte;
    float damage;

    void Start()
    {
        if (gameObject.tag == "AtaqueInimigo")
        {
            ataque_alcance = true;
            damage = 2.0f;
        }
        else
        {
            ataque_boss = true;
            damage = 8.0f;
        }
    }

    void Update()
    {
        if (ataque_boss) 
        {
            GameObject inimigos = GameObject.Find("InimigoBoss(Clone)");
            if (inimigos == null)
            {
                segunda_parte = true;
            }

            if (!segunda_parte)
            {
                damage = 1.0f;
            }
            else
            {
                damage =2.0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthPlayer.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "Chão" || collision.gameObject.name=="Cave")
        {
            Destroy(gameObject);
        }

    }
}
