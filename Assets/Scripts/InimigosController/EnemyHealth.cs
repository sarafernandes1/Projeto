using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float vida = 15;
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
                vida = 20;
            }
        }

        if (transform.tag == "InimigoBoss")
        {
            vida = 30;
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
            MudarCena.n_inimigos += 1;
            var drop = Instantiate(cube, new Vector3(transform.position.x, 6.0f, transform.position.z), cube.transform.rotation);
            float numero_random = Random.value;
                drop.tag = "Recurso";
        }
        GameObject.Destroy(gameObject);
    }

    private void OnParticleCollision(GameObject other)
    { 
        if (other.gameObject.name == "Rajadadevento")
        {
            vida -= 10*Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "AtaqueNormal(Clone)")
        {
            if (GuardarInformacao.dano_ataquenormal != 2) dano_atqnormal = GuardarInformacao.dano_ataquenormal;
            vida -= dano_atqnormal;
        }
        if (collision.gameObject.name == "Boladefogo(Clone)")
        {
            AtaqueBolaFogo(collision.gameObject);
        }
        if (collision.gameObject.name == "RaioEletrico(Clone)")
        {
            vida -= 4;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.name == "ParededePedra")
        {
            vida -= (1.5f * Time.deltaTime);
        }
    }

    public void AtaqueEletrico(GameObject raioeletrico)
    {
        vida -= 4;
        raioeletrico.SetActive(false);
    }

    public void AtaqueBolaFogo(GameObject bolafogo)
    {
        if (inimigo1)
        {
            vida -= 2;
        }

        if (inimigo2 && inimigo3)
        {
            vida-=3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Explosao(Clone)")
        {
            vida -= 20 * Time.deltaTime;
        }
    }


}
