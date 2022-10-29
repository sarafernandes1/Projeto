using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    public Slider qtd_vida;
    public static float vida;
    public Text recursos_text;
    public bool isdead;
    bool segunda_parte;

    void Start()
    {
        if (MudarCena.cena_boss)
        {
            GuardarInformacao.GetDados();
            vida = GuardarInformacao.vida;
            qtd_vida.value = vida;
        }
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
            isdead = true;
        }
    }

    public void TakeDamage(float damage)
    {
        qtd_vida.value -= damage * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Fogo")
        {
            if (!segunda_parte)
            {
                qtd_vida.value -= 0.01f * Time.deltaTime;
            }
            else
            {
                qtd_vida.value -= 0.05f * Time.deltaTime;
            }
        }

        if (other.tag == "Vida")
        {
            qtd_vida.value += 0.05f * Time.deltaTime;
        }
    }

   

}
