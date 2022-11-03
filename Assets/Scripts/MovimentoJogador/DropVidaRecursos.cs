using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropVidaRecursos : MonoBehaviour
{
    public Text qtd_recurso;
    public Slider qtd_vida;
    public static int recursos = 0;

    void Start()
    {
        if (MudarCena.cena_boss)
        {
            GuardarInformacao.GetDados();
            recursos = GuardarInformacao.recursos;
            qtd_recurso.text = recursos.ToString();
        }
    }


    void Update()
    {
        recursos = int.Parse(qtd_recurso.text.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cube(Clone)")
        {
            if (other.tag == "Recurso")
            {
                recursos++;
                qtd_recurso.text = recursos.ToString();
            }
            Destroy(other.gameObject);

        }
    }
}
