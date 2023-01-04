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
        fim_jogo = GameObject.Find("Vitoria").GetComponent<Canvas>();
    }

    void Update()
    {
        if (qtd_vida_boss.value <= 0)
        {
            StartCoroutine(espera());
        }
    }

    IEnumerator espera()
    {
        yield return new WaitForSeconds(6);
        fim_jogo.enabled = true;
        Cursor.visible = true;
    }

    public void Continuar()
    {
        SceneManager.LoadScene(0);
    }
}
