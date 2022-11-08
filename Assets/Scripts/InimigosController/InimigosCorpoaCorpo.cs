using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigosCorpoaCorpo : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public GameObject player;
    float speed, dist_max;
    bool inimigo_corpoacorpo, inimigo_alcance, inimigo_corpoBoss, inimigo_alcanceBoss;
    bool player_in_area, rajada_on;

    Rigidbody rigidbody_;
    Vector3 inicial_position;

    float cooldownTime = 2, tempo_rajada = 1.0f, cooldownataque=3; //tempo de animação ataque
    float thrust=0.0f;
    float nextFireTime = 0, timer=0, timer_ataque=0;

    void Start()
    {
        player = GameObject.Find("Player");
        if (transform.tag == "InimigoCorpoaCorpo")
        {
            speed = 3.2f;
            dist_max = 22.0f;
            inimigo_corpoacorpo = true;
        }

        if (transform.tag == "InimigoAlcance")
        {
            speed = 0.7f;
            dist_max = 22.0f;
            inimigo_alcance = true;
            if (transform.name == "AlcanceInimigo(Clone)")
            {
                inimigo_alcance = false;
                inimigo_alcanceBoss = true;
                speed = 1.0f;
            }

        }

        if (transform.tag == "InimigoBoss")
        {
            inimigo_corpoBoss = true;
            inimigo_corpoacorpo = false;
            inimigo_alcance = false;
            speed = 3.0f;
        }

        if (inimigo_corpoacorpo || inimigo_corpoBoss)
        {
            rigidbody_ = gameObject.GetComponent<Rigidbody>();
        }

        inicial_position = transform.position;

    }


    void Update()
    {
        transform.LookAt(player.transform);

        //Inimigo Corpo a Corpo
        if (inimigo_corpoacorpo)
        {
            AtaqueCorpoaCorpo();
        }

        //Inimigo Alcance
        if (inimigo_alcance && player_in_area)
        {
            if (Time.time > nextFireTime)
            {
                AtaqueAlcance();
            }
        }

        //Inimigo área boss
        if (inimigo_corpoBoss)
        {
            Perseguir();
            if (Time.time > timer_ataque)
            {
                Ataque();
            }
        }

        if (inimigo_alcanceBoss)
        {
            if (Time.time > nextFireTime && player_in_area)
            {
                AtaqueAlcance();
            }
            Perseguir();
        }


        if (inimigo_corpoacorpo || inimigo_corpoBoss)
        {
            Vector3 forward = new Vector3(-transform.forward.x, 0.0f, -transform.forward.z);
            rigidbody_.AddForce(forward * thrust*Time.deltaTime);
            if (rajada_on)
            {
                thrust = 1000.0f;
                if (Time.time > timer)
                {
                    thrust = 0.0f;
                    rajada_on = false;
                };
            }
        }

        //Inimigos voltam à sua posição original, quando player morre
        if (HealthPlayer.vida<=0)
        {
            transform.position = inicial_position;
        }
    }

    void AtaqueCorpoaCorpo()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        Ray inimigo_ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward * 6.0f));

        if (Physics.Raycast(inimigo_ray, 8.0f))
        {
            speed = 5.0f;
        }

        if (Physics.Raycast(inimigo_ray, 2.0f))
        {
            speed = 1.0f;

        }

        if (Physics.Raycast(inimigo_ray, 2.0f))
        {

            if (Time.time > timer_ataque)
            {
                Ataque();
            }
        }

        if (distanceToPlayer <= dist_max && inimigo_corpoacorpo)
        {
            Perseguir();
        }
        else
        {
            Normal();
        }
    }

    void Ataque()
    {
        RaycastHit hit;
        Ray inimigo_ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward * 2.0f));
        if (Physics.Raycast(inimigo_ray, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                HealthPlayer.TakeDamage(2.0f);
                timer_ataque = Time.time + cooldownataque;
            }
        }

    }

    void Perseguir()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void Normal()
    {
        transform.position += transform.forward * 0 * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
    }

    void AtaqueAlcance()
    {
        var projectile = Instantiate(bulletPrefab, transform.position, transform.rotation);
        projectile.velocity = transform.forward * 100;
        nextFireTime = Time.time + cooldownTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "Rajadadevento")
        {
            rajada_on = true;
            timer = Time.time + tempo_rajada;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") player_in_area = true;
        if (other.gameObject.name == "Rajadadevento" && !rajada_on)
        {
            rajada_on = true;
            timer = Time.time + tempo_rajada;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") player_in_area = false;
    }

   
}
