using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPortaCenaPrincipal : MonoBehaviour
{
    public GameObject e1, e2, e3, e4;
    bool player_defeat_enemy;
    public Animator portao1, portao2;

    void Start()
    {
    }

    void Update()
    {
        if(e1==null && e2==null && e3 == null && e4==null)
        {
            player_defeat_enemy = true;
        }

        if (player_defeat_enemy)
        {
            portao1.SetBool("abrir_portao1", true);
            portao2.SetBool("abrir_portao2", true);
        }

    }

}
