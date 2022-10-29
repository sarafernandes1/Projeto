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

    private void Update()
    {
        numero_recursos = int.Parse(qtd_recursos.text.ToString());
    }

    public static void SaveDadosPlayer()
    {
        PlayerPrefs.SetFloat("vida", HealthPlayer.vida);
        PlayerPrefs.SetFloat("manaspeed", ManaControlador.speed);
        PlayerPrefs.SetInt("recursos",numero_recursos);
    }


    public static void GetDados()
    {
        vida = PlayerPrefs.GetFloat("vida");
        speed = PlayerPrefs.GetFloat("manaspeed");
        recursos = PlayerPrefs.GetInt("recursos");
    }

}
