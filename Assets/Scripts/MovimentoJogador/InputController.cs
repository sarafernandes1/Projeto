using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    private PlayerControls _playerControls;
   
    void Awake()
    {
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    public Vector2 GetPlayerMoviment()
    {
        return _playerControls.Player.Moviment.ReadValue<Vector2>();
    }

    public bool GetPlayerJumpInThisFrame()
    {
        return _playerControls.Player.Jump.triggered;
    }

    public Vector2 GetPlayerLook()
    {
        return _playerControls.Player.Look.ReadValue<Vector2>();
    }

    
    public bool GetPlayerSairEsc()
    {
        return _playerControls.Player.SairEsc.triggered;
    }
    
    public int GetFeiticoNumber()
    {
        if (_playerControls.Player.Feiticos1.triggered) return 1;
        if (_playerControls.Player.Feiticos2.triggered) return 2;
        if (_playerControls.Player.Feiticos3.triggered) return 3;
        if (_playerControls.Player.Feiticos4.triggered) return 4;
        if (_playerControls.Player.Feiticos5.triggered) return 5;
        if (_playerControls.Player.AtaqueNormal.triggered) return 0;
        else return -1;
    }


    private void OnDisable()
    {
        _playerControls.Disable();
    }
}
