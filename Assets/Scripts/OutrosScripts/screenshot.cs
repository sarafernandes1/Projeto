using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class screenshot : MonoBehaviour
{
    private string _caminho;
    InputController inputController;

    void Start()
    {
        inputController = GameObject.Find("InputController").GetComponent<InputController>();
        _caminho = Application.dataPath + "/capturas/";

        if (!Directory.Exists(_caminho))
        {
            Directory.CreateDirectory(_caminho);
        }
    }


    void Update()
    {
        if (inputController.Captura())
        {

            string nomeImagem = _caminho + DateTime.Now.Ticks.ToString() + ".png";
            //O recurso Application.Cap...está obsoleta na versão 2017 da Unity.

            ScreenCapture.CaptureScreenshot(nomeImagem, 2); //Unity >= 2017
        }
    }
}
