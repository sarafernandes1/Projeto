using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarCena : MonoBehaviour
{
    public static bool cena_boss;
    public GameObject guardar;

    void Start()
    {
    }

    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        GuardarInformacao.SaveDadosPlayer();
        Cursor.visible = false;
        DontDestroyOnLoad(guardar);
        SceneManager.LoadScene(2);
        cena_boss = true;
    }
}
