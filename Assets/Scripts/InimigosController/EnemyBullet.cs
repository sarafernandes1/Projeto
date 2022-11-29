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
            damage = 4.0f;
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

        Ray ray = new Ray(transform.position, transform.forward * 2.0f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "InimigoBoss")
            {
                Destroy(gameObject);
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

        if (collision.gameObject.name == "Plane.001" || collision.gameObject.name=="Cave" || collision.gameObject.name=="Arena")
        {
            Destroy(gameObject);
        }

    }
}
