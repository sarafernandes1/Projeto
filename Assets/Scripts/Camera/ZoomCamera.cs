using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    public int zoom = 20;
    public int normal = 60;
    public float smooth = 5;

    private bool isZoomed = false;
    private bool notZoomed = true;
    InputController inputController;


    private void Start()
    {
        inputController = GameObject.Find("InputController").GetComponent<InputController>();
    }

    void Update()
    {
        if (inputController.GetCameraZoom())
        {
            isZoomed = !isZoomed;
        }

        if (isZoomed)
        {
            GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = Mathf.Lerp(GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView, zoom, Time.deltaTime * smooth);
        }

        if (inputController.GetCameraZoom())
        {
            isZoomed = !notZoomed;
        }

        if (notZoomed)
        {
            GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = Mathf.Lerp(GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView, normal, Time.deltaTime * smooth);
        }
    }

}
