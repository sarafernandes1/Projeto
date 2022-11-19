using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    public static Slider qtd_vida;
    Transform player;
    public Text recursos_text;
    Canvas canvas_defeat;
    public Transform[] pontos_respawn;

    Vector3 position=new Vector3();

    public Canvas boss_vida;

    bool isdead, segunda_parte;
    public static float vida;

    void Start()
    {
        if (MudarCena.cena_boss)
        {
            GuardarInformacao.GetDados();
            vida = GuardarInformacao.vida;
            qtd_vida.value = vida;

        }
        player = GameObject.Find("Player").GetComponent<Transform>();
        qtd_vida = GameObject.Find("HealthBar").GetComponent<Slider>();
       if(vida>0) qtd_vida.value = vida;
        canvas_defeat = GameObject.Find("GameOver").GetComponent<Canvas>();

    }

    void Update()
    {
        vida = qtd_vida.value;
        GameObject inimigos = GameObject.Find("InimigoBoss(Clone)");
        if (inimigos == null)
        {
            segunda_parte = true;
        }

        if (qtd_vida.value <= 0)
        {
            isDead();
            isdead = true;
        }

        if (isdead)
        {
            float min_dist = 10000;
            foreach (Transform pontos in pontos_respawn)
            {
                float distancia;
                distancia = Vector3.Distance(transform.position, pontos.position);
                if (distancia < min_dist)
                {
                    position = pontos.position;
                    min_dist = distancia;
                }
            }
            position = new Vector3(position.x, 1.0f, position.z);
            GetComponent<CharacterController>().enabled = false;
            transform.position = position;
            GetComponent<CharacterController>().enabled = true;
            isdead = false;
        }
    }


    public void isDead()
    {
        if(MudarCena.cena_boss) boss_vida.enabled = false;
        canvas_defeat.enabled = true;
        Cursor.visible = true;
    }

    public static void TakeDamage(float damage)
    {
        qtd_vida.value -= damage * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.name == "Fogo")
        //{
        //    if (!segunda_parte)
        //    {
        //        qtd_vida.value -= 0.01f * Time.deltaTime;
        //    }
        //    else
        //    {
        //        qtd_vida.value -= 0.05f * Time.deltaTime;
        //    }
        //}

        if (other.gameObject.name == "Portal2Ronda")
        {
            qtd_vida.value -= 0.01f * Time.deltaTime;
        }

        if (other.tag == "Vida")
        {
            qtd_vida.value += 0.05f * Time.deltaTime;
        }

        if (other.tag == "AtaqueEspecial")
        {
            qtd_vida.value -= 0.08f * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name=="FogoBoss(Clone)")
        {
            qtd_vida.value -= 0.05f;

        }

        if (collision.gameObject.name == "AtaqueFogo")
        {
            qtd_vida.value -= 0.05f;

        }
    }

    public void ButtonRestart()
    {
        qtd_vida.value = 1.0f;
        canvas_defeat.enabled = false;
        Cursor.visible = false;

        if(MudarCena.cena_boss) boss_vida.enabled = true;
    }

}
