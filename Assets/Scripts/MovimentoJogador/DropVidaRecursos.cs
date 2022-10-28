using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropVidaRecursos : MonoBehaviour
{
    public Text qtd_recurso;
    public Slider qtd_vida;
    int recursos = 0;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cube(Clone)")
        {
            if (other.tag == "Vida")
            {
                qtd_vida.value += 0.1f * Time.deltaTime;
            }
            if (other.tag == "Recurso")
            {
                recursos++;
                qtd_recurso.text = recursos.ToString();
            }
            Destroy(other.gameObject);

        }
    }
}
