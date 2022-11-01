using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigosCorpoaCorpo : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public GameObject player;
    float speed, dist_max;
    bool inimigo1, inimigo2, player_in_area, inimigo3, inimigo4, rajada_on;
    public bool a;

    Rigidbody rigidbody_;

    float cooldownTime = 2, tempo_rajada = 1.0f;
    public float thrust=0.0f;
    float nextFireTime = 0, timer=0;

    void Start()
    {
        player = GameObject.Find("Player");
        if (transform.tag == "InimigoCorpoaCorpo")
        {
            speed = 2.0f;
            dist_max = 14.0f;
            inimigo1 = true;
        }

        if (transform.tag == "InimigoAlcance")
        {
            speed = 0.6f;
            dist_max = 16.0f;
            inimigo2 = true;
            if (transform.name == "AlcanceInimigo(Clone)")
            {
                inimigo2 = false;
                inimigo4 = true;
                speed = 1.0f;
            }

        }

        if (transform.tag == "InimigoBoss")
        {
            inimigo3 = true;
            inimigo1 = false;
            inimigo2 = false;
            speed = 3.0f;
        }

        if (inimigo1 || inimigo3)
        {
            rigidbody_ = gameObject.GetComponent<Rigidbody>();
        }

    }


    void Update()
    {
        transform.LookAt(player.transform);


        //Inimigo Corpo a Corpo
        if (inimigo1)
        {
            AtaqueCorpoaCorpo();
        }

        //Inimigo Alcance
        if (inimigo2 && player_in_area)
        {
            if (Time.time > nextFireTime)
            {
                AtaqueAlcance();
            }
        }

        if (inimigo4 )
        {
            if (Time.time > nextFireTime && player_in_area)
            {
                AtaqueAlcance();
            }
            Perseguir();
        }

        //Inimigo área boss
        if (inimigo3)
        {
            Perseguir();
        }

        if (inimigo1 || inimigo3)
        {
            rigidbody_.AddForce(-Vector3.right * thrust);
            if (rajada_on)
            {
                thrust = 5.0f;
                if (Time.time > timer)
                {
                    thrust = 0.0f;
                    rajada_on = false;
                };
            }
        }
    }

    void AtaqueCorpoaCorpo()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        Ray inimigo_ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward * 6.0f));

        if (Physics.Raycast(inimigo_ray, 6.0f))
        {
            speed = 5.0f;
        }

        if (Physics.Raycast(inimigo_ray, 2.0f))
        {
            speed = 1.0f;
        }

        if (distanceToPlayer <= dist_max && inimigo1)
        {
            Perseguir();
        }
        else
        {
            Normal();
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
        if (other.gameObject.name == "Rajadadevento")
        {
            thrust = 4.0f;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") player_in_area = false;
    }

   
}
