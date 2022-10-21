using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float vida = 100;
    public ParticleSystem explosao;
    bool isdead = false, explosao_play;
    bool inimigo1, inimigo2, inimigo3;
    public bool a;
    //public GameObject drop;

    void Start()
    {
        if (transform.tag == "InimigoCorpoaCorpo")
        {
            inimigo1 = true;
        }

        if (transform.tag == "InimigoAlcance")
        {
            inimigo2 = true;
            vida = 200;
        }

        if (transform.tag == "InimigoBoss")
        {
            vida = 200;
            inimigo3 = true;
        }
    }

    void Update()
    {
        if (vida <= 0)
        {
            isdead = true;
            IsDead();
        }

        //Quando bola de fogo explode, tira vida aos outros inimigos
        float distanceToExplosion = Vector3.Distance(transform.position, explosao.transform.position);
        if (explosao.isEmitting)
        {
            explosao_play = true;
        }
        if (explosao_play)
        {
            if (distanceToExplosion < 5.0f)
            {
                Explosao();
            }
        }
    }

    public void IsDead()
    {
        //drop.SetActive(true);
        //drop.transform.position = gameObject.transform.position;
        GameObject.Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "AtaqueNormal")
        {
            vida -= 2;
        }
        if (other.gameObject.name == "Boladefogo")
        {
            AtaqueBolaFogo(other);
            other.SetActive(true);
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

    public void Explosao()
    {
        explosao_play = false;
        vida -= 5*Time.deltaTime;
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
