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
    public SphereCollider explosao_collider, rajada_collider;
    public Button[] buttons;
    public GameObject rajada_particle;
    public ParticleSystem rajada_;

    public int qtd_recursos;
    public int a;

    void Start()
    {
       
    }

    void Update()
    {
        if (recursos==null)
        {
            recursos = GameObject.FindGameObjectWithTag("Recurso").GetComponent<Text>();
            qtd_mana = GameObject.FindGameObjectWithTag("ManaSlider").GetComponent<Slider>();
            explosao_collider = GameObject.FindGameObjectWithTag("BolaFogoExplosao").GetComponent<ParticleSystem>().GetComponent<SphereCollider>();
            rajada_particle = GameObject.FindGameObjectWithTag("Rajada");
            rajada_ = GameObject.FindGameObjectWithTag("Rajada").GetComponent<ParticleSystem>();
            rajada_collider = GameObject.FindGameObjectWithTag("Rajada").GetComponent<SphereCollider>();

        }

        qtd_recursos = int.Parse(recursos.text.ToString());
        if (inputController.GetMelhoria())
        {
            melhoria_canvas.enabled = !melhoria_canvas.enabled;
            Cursor.visible = !Cursor.visible;
            if (melhoria_canvas.enabled)
            {
                Time.timeScale = 0.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
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

    public void ButtonAN()
    {
        if (RecursosSuficientes(2,0))
        {
            ataquenormal.value -= 0.5f;
            EnemyHealth.dano_atqnormal += 0.5f; // aumentar dano do ataque
        }
        if (ataquenormal.value == 0)
        {
            buttons[0].interactable = false;
            buttons[0].name = "bloqueado";
        }

    }

    public void ButtonF1()
    {
        if (RecursosSuficientes(2, 1))
        {
            //Bola de fogo
            bolafogo.value -= 0.5f;
            explosao_collider.radius += 1.0f; // aumentar raio de dano
        }
        if (bolafogo.value == 0)
        {
            buttons[1].interactable = false;
            buttons[1].name = "bloqueado";
        }

    }

    public void ButtonF2()
    {
        if (RecursosSuficientes(2,2))
        {
            //Raio elétrico
            raio.value -= 0.5f;
            RaioEletrico.mana_gasto -= 0.1f; // diminuir a mana necessária
        }

        if (raio.value == 0)
        {
            buttons[2].interactable = false;
            buttons[2].name = "bloqueado";
        }

    }

    public void ButtonF3()
    {
        if (RecursosSuficientes(2,3))
        {
            //Parede
            parede.value -= 0.5f;
            ParededePedra.cooldownTime -= 0.2f;
        }
        if (parede.value == 0)
        {
            buttons[3].interactable = false;
            buttons[3].name = "bloqueado";
        }

    }

    public void ButtonF4()
    {
        if (RecursosSuficientes(2,4))
        {
            //Rajada de vento
            rajada.value -= 0.5f;
            rajada_collider.radius += 1;
            rajada_collider.center =new Vector3(rajada_collider.center.x, rajada_collider.center.y, rajada_collider.center.z+1.0f);
        }

        if (rajada.value == 0)
        {
            buttons[4].interactable = false;
            buttons[4].name = "bloqueado";
        }
    }

    public void ButtonF5()
    {
        if (RecursosSuficientes(2,5))
        {
            //Fúria ancestral
            furia.value -= 0.5f;
            FuriaAncestral.cooldownTime += 0.3f; // aumentar tempo de duração
        }
        if (furia.value == 0)
        {
            buttons[5].interactable = false;
            buttons[5].name = "bloqueado";
        }

    }

    //Mana leva menos tempo a recarregar 
    public void ButtonManaPressed()
    {
        if (RecursosSuficientes(2,6))
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
