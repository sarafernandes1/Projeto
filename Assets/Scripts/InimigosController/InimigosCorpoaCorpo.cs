using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigosCorpoaCorpo : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public GameObject player;
    public Transform arco;
    float speed, dist_max;
    bool inimigo_corpoacorpo, inimigo_alcance, inimigo_corpoBoss, inimigo_alcanceBoss;
    bool player_in_area, rajada_on;

    Rigidbody rigidbody_;
    Vector3 inicial_position;

    float cooldownTime = 2, tempo_rajada = 0.6f, cooldownataque=2; //tempo de animação ataque
    public static float thrust=500.0f;
    float thrust_inicial = 0.0f;
    float nextFireTime = 0, timer=0, timer_ataque=0;

    void Start()
    {
        player = GameObject.Find("Player");
        inicial_position = transform.position;

        #region InimigoCorpoaCorpo
        if (transform.tag == "InimigoCorpoaCorpo")
        {
            speed = 3.0f;
            dist_max = 22.0f;
            inimigo_corpoacorpo = true;
        }
        #endregion

        #region InimigoAlcance
        if (transform.tag == "InimigoAlcance")
        {
            speed = 0.7f;
            dist_max = 22.0f;
            inimigo_alcance = true;
            if (transform.name == "InimigoAlcanceBoss(Clone)")
            {
                inimigo_alcance = false;
                inimigo_alcanceBoss = true;
                speed = 1.5f;
            }
        }
        #endregion

        #region InimigoCorpoBoss
        if (transform.tag == "InimigoBoss")
        {
            inimigo_corpoBoss = true;
            inimigo_corpoacorpo = false;
            inimigo_alcance = false;
            speed = 3.2f;
            thrust = 700.0f;
        }
        #endregion

        if (inimigo_corpoacorpo || inimigo_corpoBoss)
        {
            rigidbody_ = gameObject.GetComponent<Rigidbody>();
        }
    }


    void Update()
    {
        Vector3 look = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        if (inimigo_alcance /*|| inimigo_alcanceBoss*/) look = player.transform.position;
        transform.LookAt(look);

        //Inimigo Corpo a Corpo
        if (inimigo_corpoacorpo)
        {
            AtaqueCorpoaCorpo();
        }

        //Inimigo Alcance
        if (inimigo_alcance)
        {
            if (Time.time > nextFireTime && player_in_area)
            {
                AtaqueAlcance();
            }
        }

        // Inimigo Corpo a corpo área boss
        if (inimigo_corpoBoss)
        {
            Perseguir();
            if (Time.time > timer_ataque)
            {
                Ataque();
            }
        }
        
        //Inimigo alcance área boss
        if (inimigo_alcanceBoss)
        {
            Perseguir();
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (Time.time > nextFireTime && distanceToPlayer < 40.0f)
            {
                AtaqueAlcance();
            }
        }

        //Impacto Vento
        if (inimigo_corpoacorpo || inimigo_corpoBoss)
        {
            Vector3 forward = new Vector3(-transform.forward.x, 0.0f, -transform.forward.z);
            rigidbody_.AddForce(forward * thrust_inicial*Time.deltaTime);
            if (rajada_on)
            {
                thrust_inicial = thrust;
                if (Time.time > timer)
                {
                    thrust_inicial = 0.0f;
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
                HealthPlayer.TakeDamage(2.5f);
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
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void AtaqueAlcance()
    {
        Vector3 position=arco.position;
        Vector3 forward = arco.forward;
        var projectile = Instantiate(bulletPrefab, position, arco.rotation);
        projectile.velocity = forward * 100;
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
