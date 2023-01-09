using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigosCorpoaCorpo : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public GameObject player;
    public Transform arco;
    public Transform machado;

    float speed, dist_max;
    bool inimigo_corpoacorpo, inimigo_alcance, inimigo_corpoBoss, inimigo_alcanceBoss;
   bool player_in_area, rajada_on;
    bool ataque=false;

    Rigidbody rigidbody_;
    Vector3 inicial_position;

    float cooldownTime = 3, tempo_rajada = 0.10f, cooldownataque=2; //tempo de animação ataque
    public static float thrust=10.0f;
    float thrust_inicial = 0.0f;
    float nextFireTime = 0, timer=0, timer_ataque=0;

    public static float forca = 20.0f;
    bool atingido, pode_atacar=false;
    public Animator animator;

    public AudioSource audio, andar;

    void Start()
    {
        player = GameObject.Find("Player");
        inicial_position = transform.position;

        #region InimigoCorpoaCorpo
        if (transform.tag == "InimigoCorpoaCorpo")
        {
            speed = 3.0f;
            dist_max = 24.0f;
            inimigo_corpoacorpo = true;
        }
        #endregion

        #region InimigoAlcance
        if (transform.tag == "InimigoAlcance")
        {
            speed = 0.7f;
            dist_max = 22.0f;
            inimigo_alcance = true;
           
        }

        if (transform.tag == "AlcanceBoss"
               || transform.name == "Skeleton_Ranged_2_Fixed Variant(Clone)")
        {
            inimigo_alcance = false;
            inimigo_alcanceBoss = true;
            speed = 2.5f;
        }
        #endregion

        #region InimigoCorpoBoss
        if (transform.tag == "InimigoBoss")
        {
            inimigo_corpoBoss = true;
            inimigo_corpoacorpo = false;
            inimigo_alcance = false;
            speed = 3.0f;
            thrust = 700.0f;
        }
        #endregion

        if (inimigo_corpoacorpo || inimigo_corpoBoss)
        {
            rigidbody_ = gameObject.GetComponent<Rigidbody>();
        }

        if (GuardarInformacao.thrust != 20.0f)
            forca = GuardarInformacao.thrust;

       // audio = GetComponent<AudioSource>();
    }


    void Update()
    {
        Vector3 look = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(look);

        //Inimigo Corpo a Corpo
        if (inimigo_corpoacorpo)
        {
            AtaqueCorpoaCorpo();
        }

        //Inimigo Alcance
        if (inimigo_alcance)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= 70.0f || player_in_area)
            {
                animator.SetBool("combate", true);
            }

            if (Time.time > nextFireTime && player_in_area)
            {
                animator.SetBool("ataque", true);
                StartCoroutine(espera2());
                if(pode_atacar) AtaqueAlcance();
            }
            else
            {
                pode_atacar = false;
            }

            if (!player_in_area)
            {
                animator.SetBool("ataque", false);
                animator.SetBool("combate", false);
            }
        }

        // Inimigo Corpo a corpo área boss
        if (inimigo_corpoBoss)
        {
            AtaqueCorpoaCorpo();
        }
        
        //Inimigo alcance área boss
        if (inimigo_alcanceBoss)
        {
            AtaqueInimigoAlcance();
        }

        //Impacto Vento
        if (inimigo_corpoacorpo || inimigo_corpoBoss)
        {
            if (rajada_on)
            {
                transform.position -= transform.forward * Time.deltaTime * forca;
                if (Time.time > timer)
                {
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

        if (distanceToPlayer <= 5.0f) andar.mute = true;

        if (/*Physics.Raycast(inimigo_ray, 4.0f) &&*/ distanceToPlayer<4.0f)
        {
            Normal();
            animator.SetBool("correr", false);
            animator.SetBool("combate", true);


            if (Time.time > timer_ataque)
            {
                animator.SetBool("ataque", true);

                StartCoroutine(espera4());
                if (pode_atacar)
                {
                    audio.Play();

                    Ataque();
                }
                //ataque = true;
            }
            else
            {
                pode_atacar = false;
                animator.SetBool("ataque", false);
            }
        }

        if ((distanceToPlayer<=25.0f && distanceToPlayer>=4.0f) || atingido && distanceToPlayer>=4.0f || inimigo_corpoBoss && distanceToPlayer>=4.0f)
        {
            animator.SetBool("correr", true);
            animator.SetBool("idle", false);
            animator.SetBool("combate", false);
            animator.SetBool("ataque", false);
            if(distanceToPlayer>=6.0f) andar.mute = false;
            ataque = false;
            Perseguir();
        }
        else
        {
            if (distanceToPlayer > 25.0f)
            {
                andar.mute = true;
                Normal();
                animator.SetBool("idle", true);
                animator.SetBool("correr", false);
                animator.SetBool("combate", false);
                animator.SetBool("ataque", false);

            }
        }
    }

    void AtaqueInimigoAlcance()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= 60.0f && distanceToPlayer >= 8.0f)
        {
            animator.SetBool("correr", true);
            animator.SetBool("ataque", false);
            animator.SetBool("combate", false);

            Perseguir();
        }

        if (distanceToPlayer < 7.0f)
        {
            andar.mute = true;
        }

        if (Time.time > nextFireTime && distanceToPlayer <8.0f)
        {

            animator.SetBool("combate", true);
            StartCoroutine(espera3());
            if (pode_atacar)
            {
                AtaqueAlcance();
            }
        }
        else
        {
            pode_atacar = false;
        }


    }

    IEnumerator damage()
    {
        yield return new WaitForSeconds(0.5f);
        if(!machado) HealthPlayer.TakeDamage(3.5f);
        else HealthPlayer.TakeDamage(2.5f);
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(0.7f);
        animator.SetBool("ataque", false);

    }

    IEnumerator espera3()
    {

        animator.SetBool("ataque", true);
        yield return new WaitForSeconds(2.0f);
        pode_atacar = true;

    }

    
    IEnumerator espera2()
    {
        yield return new WaitForSeconds(2.0f);
        pode_atacar = true;

    }

    IEnumerator espera4()
    {
        yield return new WaitForSeconds(0.5f);
        pode_atacar = true;

    }

    void Ataque()
    {
        //float distance = Vector3.Distance(player.transform.position, transform.position);
        //if (distance <= 3.5f)
        //{
            animator.SetBool("ataque", true);
            speed = 0.0f;
        if (!machado) HealthPlayer.TakeDamage(3.5f);
        else HealthPlayer.TakeDamage(2.5f);
        //  StartCoroutine(damage());
        timer_ataque = Time.time + cooldownataque;
        //}
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
        audio.Play();
        var bolaFogo = Instantiate(bulletPrefab, arco.transform.position, /*Quaternion.identity*/ arco.transform.rotation);
        bolaFogo.velocity = (player.transform.position - arco.transform.position).normalized * 80.0f;
        StartCoroutine(espera());
        nextFireTime = Time.time + cooldownTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "AtaqueNormal(Clone)")
        {
            atingido = true;
        }
        if (collision.gameObject.name == "Boladefogo(Clone)")
        {
            atingido = true;

        }
        if (collision.gameObject.name == "RaioEletrico(Clone)")
        {
            atingido = true;

        }

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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") player_in_area = false;
    }
}
