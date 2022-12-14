using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigarDesligarLuz : MonoBehaviour
{
    Light luz_tocha;
    bool vento_ativo, bola_luz;
    public ParticleSystem fogo, fumo;

    void Start()
    {
        luz_tocha = gameObject.GetComponent<Light>();
    }

    void Update()
    {
        if (vento_ativo)
        {
            luz_tocha.enabled = false;
            fogo.Stop();
            fumo.Play();
            vento_ativo = false;
        }

        if (bola_luz)
        {
            luz_tocha.enabled = true;
            fumo.Stop();
            fogo.Play();
            bola_luz = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.name == "Rajadadevento")
        {
            vento_ativo = true;
        }

        if (other.gameObject.name == "Boladefogo(Clone)")
        {
            bola_luz = true;
        }
    }



}
