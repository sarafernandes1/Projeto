using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float vida = 100;
    public ParticleSystem explosao;
    bool inimigo1, inimigo1_1, inimigo2, inimigo3;
    public bool a;
    public GameObject cube;

    public static int dano_atqnormal=2;

    void Start()
    {
        if (transform.tag == "InimigoCorpoaCorpo")
        {
            inimigo1 = true;
            if (gameObject.name == "Cylinder" || gameObject.name == "Cylinder (1)")
            {
                vida = 20;
                inimigo1 = false;
                inimigo1_1 = true;
            }
        }

        if (transform.tag == "InimigoAlcance")
        {
            if (gameObject.name == "Cylinder (2)" || gameObject.name == "Cylinder (3)")
            {
                inimigo2 = true;
                vida = 40;
            }
            else
            {
                inimigo2 = true;
                vida = 120;
            }
        }

        if (transform.tag == "InimigoBoss")
        {
            vida = 150;
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
            var drop = Instantiate(cube, new Vector3(transform.position.x, 1.0f, transform.position.z), transform.rotation);
            float numero_random = Random.value;
            //if (numero_random < 0.5f)
            //{
            //    drop.tag = "Vida";
            //}
            //else
            //{
                drop.tag = "Recurso";
           // }
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
            vida -= 30;
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
        vida -= 20;
        raioeletrico.SetActive(false);
    }

    public void AtaqueBolaFogo(GameObject bolafogo)
    {
        if (inimigo1)
        {
            vida -= 6;
        }

        if (inimigo2 && inimigo3)
        {
            vida-=5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Explosao")
        {
            vida -= 20 * Time.deltaTime;
        }
    }

    public void RajadaVento(GameObject vento)
    {
        float distanceToVento = Vector3.Distance(vento.transform.position, transform.position);

        if (distanceToVento < 12.0f)
        {
            vida -= 10;
        }
    }

}
