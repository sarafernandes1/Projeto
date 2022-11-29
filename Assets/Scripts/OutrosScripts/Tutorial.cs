using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Canvas canvas;
    public InputController InputController;
    GameObject parede;

    void Start()
    {
        parede = GameObject.Find("ParedeTutorial");
    }


    void Update()
    {
        if (InputController.GetPlayerJumpInThisFrame() && canvas.enabled)
        {
            Time.timeScale = 1.0f;
            canvas.enabled = false;
            Destroy(gameObject);
        }

        if (GameObject.Find("Cylinder") == null && GameObject.Find("Cylinder (1)")==null)
        {
            Destroy(parede.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canvas.enabled = true;
            Time.timeScale = 0.0f;
        }
    }
    
}
