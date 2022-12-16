using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CristalCura : MonoBehaviour
{
    ParticleSystem particula;
    public Light luz;
    bool player_in_area;
    public float time = 15;

    public static bool Cura=true;

    void Start()
    {
        particula = GetComponentInChildren<ParticleSystem>();
    }


    void Update()
    {
        if (player_in_area)
        {
            if(Cura) luz.intensity -= 0.8f * Time.deltaTime;
        }

        if (luz.intensity <= 0)
        {
            Cura = false;
            time -= 2.0f * Time.deltaTime;
            particula.Stop();
        }
        if (time <= 0 && luz.intensity <= 3)
        {
            luz.intensity += 1.5f * Time.deltaTime;

        }
        if (luz.intensity >= 3 && player_in_area)
        {
            time = 10;
            Cura = true;
            particula.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(Cura) particula.Play();
            player_in_area = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            particula.Stop();
            player_in_area = false;
        }
    }
}
