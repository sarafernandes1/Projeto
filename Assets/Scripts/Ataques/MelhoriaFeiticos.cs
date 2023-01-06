using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MelhoriaFeiticos : MonoBehaviour
{
    public Canvas melhoria_canvas;
    public Text recursos;
    public InputController inputController;
    public Slider mana, qtd_mana, parede, furia, ataquenormal, bolafogo, raio, rajada;
    public SphereCollider explosao_collider;
    public Button[] buttons;
    public GameObject rajada_particle, objeto_parede;

    public int qtd_recursos;
    public int a;

    public static bool mudanca_parede=false;
    public static bool gamePaused = false;
    public static float explosao_collider_raio=5f;

    void Start()
    {
       
    }

    void Update()
    {
        if (recursos==null)
        {
            recursos = GameObject.FindGameObjectWithTag("Recurso").GetComponent<Text>();
            qtd_mana = GameObject.FindGameObjectWithTag("ManaSlider").GetComponent<Slider>();
            explosao_collider = GameObject.Find("Explosao").GetComponent<SphereCollider>();
            rajada_particle = GameObject.FindGameObjectWithTag("Rajada");

        }

        qtd_recursos = int.Parse(recursos.text.ToString());
        if (inputController.GetMelhoria())
        {
            melhoria_canvas.enabled = !melhoria_canvas.enabled;
            Cursor.visible = !Cursor.visible;
            if (melhoria_canvas.enabled)
            {
                Cursor.lockState = CursorLockMode.None;

                Time.timeScale = 0.0f;
                gamePaused = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;

                Time.timeScale = 1.0f;
                gamePaused = false;

            }
        }

        if (qtd_recursos < 2)
        {
            foreach(var numero in buttons)
            {
              if(numero.name!="bloqueado")  numero.interactable = false;
            }
        }
        else
        {
            foreach (var numero in buttons)
            {
                if (numero.name != "bloqueado") numero.interactable = true;
            }
        }
    }

    public void ButtonAN() //MUDANÇA CENA COMPLETA
    {
        if (RecursosSuficientes(4,0))
        {
            ataquenormal.value -= 0.5f;
            EnemyHealth.dano_atqnormal += 0.7f; // aumentar dano do ataque
        }
        if (ataquenormal.value == 0)
        {
            buttons[0].interactable = false;
            buttons[0].name = "bloqueado";
        }

    }

    public void ButtonF1() //MUDANÇA CENA COMPLETA
    {
        if (RecursosSuficientes(2, 1))
        {
            //Bola de fogo
            bolafogo.value -= 0.5f;
            explosao_collider.radius += 1.0f; // aumentar raio de dano
            explosao_collider_raio = explosao_collider.radius;
        }
        if (bolafogo.value == 0)
        {
            buttons[1].interactable = false;
            buttons[1].name = "bloqueado";
        }

    }

    public void ButtonF2() //MUDANÇA CENA COMPLETA
    {
        if (RecursosSuficientes(3,2))
        {
            //Raio elétrico
            raio.value -= 0.5f;
            Feiticos.mana_raio -= 0.1f; // diminuir a mana necessária
        }

        if (raio.value == 0)
        {
            buttons[2].interactable = false;
            buttons[2].name = "bloqueado";
        }

    }

    public void ButtonF3() //MUDANÇA CENA COMPLETA
    {
        if (RecursosSuficientes(4,3))
        {
            //Parede
            parede.value -= 0.5f;
            ParededePedra.cooldownTime -= 0.5f;
        }
        if (parede.value == 0)
        {
            mudanca_parede = true;
            ParededePedra.objeto = objeto_parede;
            buttons[3].interactable = false;
            buttons[3].name = "bloqueado";
        }

    }

    public void ButtonF4() //MUDANÇA CENA COMPLETA
    {
        if (RecursosSuficientes(3,4))
        {
            //Rajada de vento
            rajada.value -= 0.5f;
            InimigosCorpoaCorpo.forca += 5;
        }

        if (rajada.value == 0)
        {
            buttons[4].interactable = false;
            buttons[4].name = "bloqueado";
        }
    }

    public void ButtonF5() //MUDANÇA CENA COMPLETA
    {
        if (RecursosSuficientes(5,5))
        {
            //Fúria ancestral
            furia.value -= 0.5f;
            FuriaAncestral.cooldownTime += 0.5f; // aumentar tempo de duração
        }
        if (furia.value == 0)
        {
            buttons[5].interactable = false;
            buttons[5].name = "bloqueado";
        }

    }

    //Mana leva menos tempo a recarregar 
    public void ButtonManaPressed() //MUDANÇA CENA COMPLETA
    {
        if (RecursosSuficientes(5,6))
        {
            mana.value -= 0.5f;
            ManaControlador.speed += 0.1f;
        }

        if (mana.value == 0)
        {
            buttons[6].interactable = false;
            buttons[6].name = "bloqueado";
        }

    }

    public bool RecursosSuficientes(int numero, int n_button)
    {
        if (qtd_recursos >= numero && buttons[n_button].interactable)
        {
            qtd_recursos -= numero;
            recursos.text = qtd_recursos.ToString();
            return true;
        }
        else
        {
            return false;
        }
    }
}
