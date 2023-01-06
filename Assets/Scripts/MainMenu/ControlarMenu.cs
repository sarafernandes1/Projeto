using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlarMenu : MonoBehaviour
{
    public Canvas config, menu, pausa;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGameClick() //função a ser chamada atarvés do inspector do Botão (ver Button na Scene)
    {
        Debug.Log("new game click");

        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(1); //SceneManager.LoadScene("SampleScene"); //faz load de uma nova Scene
    }

    public void ExitClick() //função a ser chamada atarvés do inspector do Botão (ver Button (1) na Scene)
    {
        Debug.Log("exit click");
        Application.Quit(); //Fecha a aplicação; não corre no editor
    }

    public void Configuracao()
    {
        menu.enabled = false;
        config.enabled = true;
    }

    public void Voltar()
    {
        config.enabled = false;
        menu.enabled = true;

    }

    public void VoltaraJogo()
    {
        pausa.enabled = false;
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        PlayerController.GamePaused = false;
    }

    public void Sair()
    {

        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
