using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPortaCenaPrincipal : MonoBehaviour
{
    //GameObject chave;
    public GameObject e1, e2, e3;
    bool player_defeat_enemy;
    MeshRenderer porta;
    //GameObject player;

    void Start()
    {
        porta = gameObject.GetComponentInChildren<MeshRenderer>();
        //player = GameObject.Find("Player");
    }

    void Update()
    {
        if(e1==null && e2==null && e3 == null)
        {
            player_defeat_enemy = true;
        }

        if (player_defeat_enemy)
        {
            Collider collider_objeto = gameObject.GetComponent<Collider>();
            collider_objeto.enabled = false;
            porta.enabled = false;
            //chave.SetActive(true);
        }

        //float distanceToChave = Vector3.Distance(player.transform.position, chave.transform.position);
        //if (distanceToChave <= 2.0f)
        //{
        //    chave.SetActive(false);
        //    porta.enabled = false;
        //    Collider collider_objeto = gameObject.GetComponent<Collider>();
        //    collider_objeto.enabled = false;
        //}
    }

}
