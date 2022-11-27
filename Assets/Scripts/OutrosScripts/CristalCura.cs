using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalCura : MonoBehaviour
{
    ParticleSystem particula;

    void Start()
    {
        particula = GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player") particula.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") particula.Stop();
    }
}
