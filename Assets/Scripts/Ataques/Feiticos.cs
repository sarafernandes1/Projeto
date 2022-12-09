using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feiticos : MonoBehaviour
{
    public InputController inputController;
    public Rigidbody bolafogo, ataquenormal, raioeletico;
    public Transform ponto;
    public Camera camera;
    public Slider qtd_mana;
    public Image imagem_tempo_BF, imagem_tempo_AN, imagem_tempo_RE;

    Vector3 destination;
    float cooldownTime_bolafogo = 2, cooldownTime_ataquenormal = 0.8f, cooldownTime_raioeletrico=2.5f;
    public static float nextFireTime_bolafogo = 0, nextFireTime_ataquenormal = 0.0f, nextFireTime_raioeletrico=0;
    bool can_atack, cooldown;
    bool can_ataque_an, cooldown_an; // ataque normal
    bool can_atack_re, cooldown_re; //raio elétrico
    public Animator animator;
    public static float mana_raio = 0.35f;

    bool descer, subir = true;
    public Vector3 p;

    void Start()
    {
        imagem_tempo_BF.fillAmount = 0;
        imagem_tempo_AN.fillAmount = 0;
        imagem_tempo_RE.fillAmount = 0;

        if (GuardarInformacao.qtd_mana_raio != 0.35f)
            mana_raio = GuardarInformacao.qtd_mana_raio;
    }


    void Update()
    {
        if (descer)
        {
            transform.position-=new Vector3(0.0f, 0.005f * Time.deltaTime,0.0f);
            StartCoroutine(espera());
        }

        if (subir)
        {
            transform.position+= new Vector3(0.0f, 0.005f * Time.deltaTime, 0.0f);
            StartCoroutine(espera1());

        }


        destination = DestinoFeitico(destination);

        if (Time.time > nextFireTime_bolafogo && !MelhoriaFeiticos.gamePaused)
        {
            PodesAtacar(0.3f);

            if (inputController.GetFeiticoNumber() == 1 && can_atack && LuzBastao.numero_feitico == -1
                || inputController.GetFeiticoNumber() == 1 && LuzBastao.numero_feitico == 5)
            {
                qtd_mana.value -= 0.3f;

                nextFireTime_bolafogo = Time.time + cooldownTime_bolafogo;
                cooldown = true;
                BoladeFogo.ataque = true;
                animator.Play("Magia_Bola_de_Fogo");

                DispararFeitico(bolafogo, 1, 30, "bolafogo");
            }
        }

        if (cooldown)
        {
            imagem_tempo_BF.fillAmount += 1 / cooldownTime_bolafogo * Time.deltaTime;

            if (imagem_tempo_BF.fillAmount >= 1)
            {
                if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = -1;
                imagem_tempo_BF.fillAmount = 0;
                cooldown = false;
            }
            BoladeFogo.ataque = false;
        }

        if (Time.time > nextFireTime_raioeletrico && !MelhoriaFeiticos.gamePaused)
        {
            PodesAtacar(mana_raio);

            if (inputController.GetFeiticoNumber() == 2 && can_atack && LuzBastao.numero_feitico == -1
                || inputController.GetFeiticoNumber() == 2 && LuzBastao.numero_feitico == 5)
            {
                qtd_mana.value -= mana_raio;

                nextFireTime_raioeletrico = Time.time + cooldownTime_raioeletrico;
                cooldown_re = true;
                RaioEletrico.ataque = true;
                animator.Play("Magia_Raio_Eletrico");

                DispararFeitico(raioeletico, 2, 65.0f, "raio");
            }
        }

        if (cooldown_re)
        {
            imagem_tempo_RE.fillAmount += 1 / cooldownTime_raioeletrico * Time.deltaTime;

            if (imagem_tempo_RE.fillAmount >= 1)
            {
                if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = -1;
                imagem_tempo_RE.fillAmount = 0;
                cooldown_re = false;
            }
            RaioEletrico.ataque = false;
        }

        if (Time.time > nextFireTime_ataquenormal && !MelhoriaFeiticos.gamePaused)
        {
            if (inputController.GetFeiticoNumber() == 0 && LuzBastao.numero_feitico == -1
                || inputController.GetFeiticoNumber() == 0 && LuzBastao.numero_feitico == 5)
            {
                nextFireTime_ataquenormal = Time.time + cooldownTime_ataquenormal;
                cooldown_an = true;
                AtaqueNormal.ataque = true;
                animator.Play("Magia_Normal");
                 DispararFeitico(ataquenormal,0,30, "normal");
            }
        }

        if (cooldown_an)
        {
            imagem_tempo_AN.fillAmount += 1 / cooldownTime_ataquenormal * Time.deltaTime;

            if (imagem_tempo_AN.fillAmount >= 1)
            {
                if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = -1;
                imagem_tempo_AN.fillAmount = 0;
                cooldown_an = false;
            }
            AtaqueNormal.ataque = false;
        }

    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(2f);
        subir = true;
        descer = false;
    }

    IEnumerator espera1()
    {
        yield return new WaitForSeconds(2f);
        subir = false;
        descer = true;
    }


    public Vector3 DestinoFeitico(Vector3 destination)
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        destination = Vector3.zero;

        if (Physics.Raycast(ray, out hit)) destination = hit.point;
        else destination = ray.GetPoint(1000);

        return destination;
    }

    public void PodesAtacar(float mana_necessaria)
    {
        if (qtd_mana.value > mana_necessaria) can_atack = true;
        else can_atack = false;
    }

    public void DispararFeitico(Rigidbody rigidbody, int num_feitico, float speed, string nome)
    {
        if (LuzBastao.numero_feitico != 5) LuzBastao.numero_feitico = num_feitico;

        var projectile = Instantiate(rigidbody, ponto.position, Quaternion.identity);
        projectile.gameObject.SetActive(true);
        projectile.gameObject.GetComponent<ParticleSystem>().Play();
        projectile.velocity = (destination - ponto.position).normalized * speed;
    }
  
}
