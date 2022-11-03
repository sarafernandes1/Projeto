using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float vida = 15;
    public ParticleSystem explosao;
    bool inimigo1,inimigo1_1, inimigo2, inimigo3;
    public GameObject cube;
    public static float dano_atqnormal=2;

    void Start()
    {
        if (transform.tag == "InimigoCorpoaCorpo")
        {
            inimigo1 = true;
            vida = 15;
            if (gameObject.name == "Cylinder" || gameObject.name == "Cylinder (1)")
            {
                vida = 15;
                inimigo1 = false;
                inimigo1_1 = true;
            }
        }

        if (transform.tag == "InimigoAlcance")
        {
            if (gameObject.name == "Cylinder (2)" || gameObject.name == "Cylinder (3)")
            {
                inimigo2 = true;
                vida = 10;
            }
            else
            {
                inimigo2 = true;
                vida = 30;
            }
        }

        if (transform.tag == "InimigoBoss")
        {
            vida = 40;
            inimigo3 = true;
        }


    }

    void Update()
    {
        if (vida <= 0)
        {
            IsDead();
        }

    }

    public void IsDead()
    {
        if (inimigo1 || inimigo3)
        {
            var drop = Instantiate(cube, new Vector3(transform.position.x, 2.0f, transform.position.z), transform.rotation);
            float numero_random = Random.value;
                drop.tag = "Recurso";
        }
        GameObject.Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "AtaqueNormal")
        {
            vida -= dano_atqnormal;
        }
        if (other.gameObject.name == "Boladefogo")
        {
            AtaqueBolaFogo(other);
            explosao.gameObject.SetActive(true);
        }
        if (other.gameObject.name == "RaioEletrico")
        {
            AtaqueEletrico(other);
            other.SetActive(true);
        }
        if (other.gameObject.name == "Rajadadevento")
        {
            vida -= 3;
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name == "ParededePedra")
        {
            vida -= (1.5f * Time.deltaTime);
        }
        if (collision.transform.tag == "Player")
        {
            HealthPlayer healthPlayer = collision.transform.GetComponent<HealthPlayer>();
            healthPlayer.TakeDamage(0.1f);
        }
    }

    public void AtaqueEletrico(GameObject raioeletrico)
    {
        vida -= 6;
        raioeletrico.SetActive(false);
    }

    public void AtaqueBolaFogo(GameObject bolafogo)
    {
        if (inimigo1)
        {
            vida -= 4;
        }

        if (inimigo2 && inimigo3)
        {
            vida-=7;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Explosao")
        {
            vida -= 20 * Time.deltaTime;
        }

        if (other.name == "Rajadadevento")
        {
            vida -= 5 * Time.deltaTime;
        }
    }


}
