using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Canvas canvas;
    public InputController InputController;

    void Start()
    {
        
    }


    void Update()
    {
        if (InputController.GetPlayerJumpInThisFrame() && canvas.enabled)
        {
            Time.timeScale = 1.0f;
            canvas.enabled = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        canvas.enabled = true;
        Time.timeScale = 0.0f;
    }
    
}
