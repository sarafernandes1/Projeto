using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaControlador : MonoBehaviour
{
    public Slider mana_slider;
    public InputController inputController;
    public static float speed = 0.1f;
    
    void Start()
    {
        if (MudarCena.cena_boss)
        {
            GuardarInformacao.GetDados();
            speed = GuardarInformacao.speed;
        }
    }

    void Update()
    {
        if(mana_slider.value<1) mana_slider.value += speed*Time.deltaTime;
    }
}
