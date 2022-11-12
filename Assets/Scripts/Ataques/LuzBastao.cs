using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzBastao : MonoBehaviour
{
    public InputController inputController;
    public Material luzbranca, luzazul, luzvermelha, luzamarela, luzlaranja, luzvento, luzverde;
    MeshRenderer luz;

    public static int numero_feitico;

    void Start()
    {
        luz = GameObject.Find("Gema").GetComponent<MeshRenderer>();
        numero_feitico = -1;
    }

    void Update()
    {
        MudarLuzBastao();
    }

    public void MudarLuzBastao()
    {
        switch (numero_feitico)
        {
            case -2:
                TempoLuz();
                numero_feitico = -1;
                break;
            case -1:
                TempoLuz();
                break;
            case 0:
                luz.material = luzazul;
                break;
            case 1:
                luz.material = luzvermelha;
                break;
            case 2:
                luz.material = luzamarela;
                break;
            case 3:
                luz.material = luzlaranja;
                break;
            case 4:
                luz.material = luzvento;
                break;
            case 5:
                luz.material = luzverde;
                break;
        }
    }

    //Passar da cor dos feitiços para a cor normal (cor branca)
    public void TempoLuz()
    {
        luz.material = luzbranca;
    }
}
