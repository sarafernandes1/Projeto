using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    bool ataque_alcance, ataque_boss;
    float damage;

    void Start()
    {
        if (gameObject.tag == "AtaqueInimigo")
        {
            ataque_alcance = true;
            damage = 5.0f;
        }
        else
        {
            ataque_boss = true;
            damage = 8.0f;
        }
    }

    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthPlayer healthPlayer = collision.transform.GetComponent<HealthPlayer>();
            healthPlayer.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.name == "Chão" || collision.gameObject.name=="Cave")
        {
            Destroy(gameObject);
        }

    }
}
