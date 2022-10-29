using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaControlador : MonoBehaviour
{
    public Slider mana_slider;
    public InputController inputController;
    float speed = 0.1f;
    

    void Start()
    {
        
    }

    void Update()
    {
        if(mana_slider.value<1) mana_slider.value += speed*Time.deltaTime;
    }
}
