using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MudarCena : MonoBehaviour
{
    public static bool cena_boss;
    public GameObject guardar, update;
    public InputController inputController;
    bool activate_newcene=true;
    public Text text;
    public static int n_inimigos = 0;
    Canvas canvas;

    void Start()
    {
        canvas = GameObject.Find("CanvasMudarCena").GetComponent<Canvas>();
    }

    void Update()
    {
        if (n_inimigos == 41)
        {
            activate_newcene = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (activate_newcene && other.gameObject.tag=="Player")
        {
            GuardarInformacao.SaveDadosPlayer();
            DontDestroyOnLoad(guardar);
            DontDestroyOnLoad(update);
            DontDestroyOnLoad(inputController);
            SceneManager.LoadScene(2);
            cena_boss = true;
        }
        else
        {
            canvas.enabled = true;
            int inimigos_faltam = 41 - n_inimigos;
            text.text = "Ainda faltam " +inimigos_faltam.ToString() + " inimigos";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.enabled = false;
    }
}
