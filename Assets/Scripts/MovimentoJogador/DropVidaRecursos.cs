using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropVidaRecursos : MonoBehaviour
{
    public Text qtd_recurso;
    public Slider qtd_vida;
    int recursos = 0;
    float vida_ou_recurso;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        vida_ou_recurso=Random.value;
        if (vida_ou_recurso <= 0.5f)
        {
            recursos++;
            qtd_recurso.text = recursos.ToString();
            other.gameObject.SetActive(false);
        }
        else
        {
            qtd_vida.value += 0.1f;
            other.gameObject.SetActive(false);

        }
    }
}
