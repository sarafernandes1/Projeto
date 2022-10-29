using System.Collections;
using System.Collections.Generic;
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

    int qtd_recursos;

    void Start()
    {

    }

    void Update()
    {
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

        if (qtd_recursos <= 0)
        {
            foreach(var numero in buttons)
            {
                numero.interactable = false;
            }
        }
    }

    public void ButtonAN()
    {
        if (RecursosSuficientes(2,0))
        {
            ataquenormal.value -= 0.5f;
            EnemyHealth.dano_atqnormal += 1; // aumentar dano do ataque
        }
        if (ataquenormal.value == 0) buttons[0].interactable = false;

    }

    public void ButtonF1()
    {
        if (RecursosSuficientes(2,1))
        {
            //Bola de fogo
            bolafogo.value -= 0.5f;
            explosao_collider.radius += 1.0f; // aumentar raio de dano
        }
        if (bolafogo.value == 0) buttons[1].interactable = false;

    }

    public void ButtonF2()
    {
        if (RecursosSuficientes(2,2))
        {
            //Raio elétrico
            raio.value -= 0.5f;
            RaioEletrico.mana_gasto -= 0.1f; // diminuir a mana necessária
        }

        if (raio.value == 0) buttons[2].interactable = false;

    }

    public void ButtonF3()
    {
        if (RecursosSuficientes(2,3))
        {
            //Parede
            parede.value -= 0.5f;
            ParededePedra.cooldownTime -= 0.2f;
        }
        if (parede.value == 0) buttons[3].interactable = false;

    }

    public void ButtonF4()
    {
        if (RecursosSuficientes(2,4))
        {
            //Rajada de vento
            rajada.value -= 0.5f;
            Rajadadevento.mana_necessario -= 0.1f;
        }

        if (rajada.value == 0) buttons[4].interactable = false;
    }

    public void ButtonF5()
    {
        if (RecursosSuficientes(2,5))
        {
            //Fúria ancestral
            furia.value -= 0.5f;
            FuriaAncestral.cooldownTime -= 0.2f; // diminuir tempo de cooldown
        }
        if (furia.value == 0) buttons[5].interactable = false;

    }

    //Mana leva menos tempo a recarregar 
    public void ButtonManaPressed()
    {
        if (RecursosSuficientes(2,6))
        {
            mana.value -= 0.5f;
            ManaControlador.speed += 0.1f;
        }

        if (mana.value == 0) buttons[6].interactable = false;

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
