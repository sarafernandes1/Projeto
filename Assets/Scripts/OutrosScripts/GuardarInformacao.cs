using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuardarInformacao : MonoBehaviour
{
    public static float vida, speed;
    public static int recursos;
    public static int numero_recursos;
    public Text qtd_recursos;
    public static GameObject parede_ativa;
    public static float dano_ataquenormal=2.0f;
    public static float tempo_cooldownparede = 4.0f;
    public static float qtd_mana_raio = 0.35f;
    public static float thrust = 700.0f;
    public static float furia_cooldown = 4.0f;
    public static float raio_explosao;

    private void Update()
    {
        numero_recursos = int.Parse(qtd_recursos.text.ToString());
        dano_ataquenormal = EnemyHealth.dano_atqnormal;
        tempo_cooldownparede = ParededePedra.cooldownTime;
        qtd_mana_raio = Feiticos.mana_raio;
        thrust = InimigosCorpoaCorpo.thrust;
        furia_cooldown = FuriaAncestral.cooldownTime;
        raio_explosao = MelhoriaFeiticos.explosao_collider_raio;
    }

    public static void SaveDadosPlayer()
    {
        PlayerPrefs.SetFloat("vida", HealthPlayer.vida);
        PlayerPrefs.SetFloat("manaspeed", ManaControlador.speed);
        PlayerPrefs.SetInt("recursos",numero_recursos);
        PlayerPrefs.SetFloat("danoataquenormal", dano_ataquenormal);
        PlayerPrefs.SetFloat("cooldownparede", tempo_cooldownparede);
        PlayerPrefs.SetFloat("manaraio", qtd_mana_raio);
        PlayerPrefs.SetFloat("thrust", thrust);
        PlayerPrefs.SetFloat("cooldownfuria", furia_cooldown);
        PlayerPrefs.SetFloat("explosaoraio", raio_explosao);

    }


    public static void GetDados()
    {
        vida = PlayerPrefs.GetFloat("vida");
        speed = PlayerPrefs.GetFloat("manaspeed");
        recursos = PlayerPrefs.GetInt("recursos");
        dano_ataquenormal = PlayerPrefs.GetFloat("danoataquenormal");
        tempo_cooldownparede = PlayerPrefs.GetFloat("cooldownparede");
        qtd_mana_raio = PlayerPrefs.GetFloat("manaraio");
        thrust = PlayerPrefs.GetFloat("thrust");
        furia_cooldown = PlayerPrefs.GetFloat("cooldownfuria");
        raio_explosao = PlayerPrefs.GetFloat("explosaoraio");
    }


}
