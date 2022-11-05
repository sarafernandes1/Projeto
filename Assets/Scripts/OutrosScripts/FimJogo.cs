using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FimJogo : MonoBehaviour
{
    public Slider qtd_vida_boss;
    Canvas fim_jogo;

    void Start()
    {
        fim_jogo = GameObject.Find("CanvasBoss").GetComponent<Canvas>();
    }

    void Update()
    {
        if (qtd_vida_boss.value <= 0)
        {
            Cursor.visible = true;
            fim_jogo.enabled = true;
        }
    }

    public void Continuar()
    {
        SceneManager.LoadScene(0);
    }
}
