using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInput inputSystem;
    CameraController cameraController;

    private void Awake()
    {
        inputSystem = new PlayerInput();
        cameraController = GetComponent<CameraController>();

        InitializeInputSystem();
    }

    private void OnEnable()
    {
        inputSystem.Player.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Player.Disable();
    }

    void InitializeInputSystem()
    {
        inputSystem.Player.Movement.performed += ctx => cameraController.SetXAxis(ctx.ReadValue<Vector2>().x);
    }
}
