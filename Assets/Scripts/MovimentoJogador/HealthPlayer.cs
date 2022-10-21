using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    public Slider qtd_vida;
    public bool isdead;

    void Start()
    {
        
    }

    void Update()
    {
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
            qtd_vida.value -= 0.01f * Time.deltaTime;
        }
    }

   

}
