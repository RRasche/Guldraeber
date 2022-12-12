using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private Camera playerCamera;

    public int UserIndex {
        get {return GetComponent<PlayerInput>().user.index;
        }
    }

    public void Start()
    {
        GameObject playerController = Instantiate(PlayerManager.Instance.controllerPrefabs[UserIndex], transform) as GameObject;
        controller = playerController.GetComponent<PlayerController>();
        this.GetComponentInChildren<CameraMovement>().setPlayerTransform(playerController.transform);
    }

    public void ChangeMoveInput(InputAction.CallbackContext context) {
        if (controller)
        {
            controller.ChangeMoveDir(context.ReadValue<Vector2>());
        }
    }

    public void ChangeLookInput(InputAction.CallbackContext context)
    {
        if (controller)
        {
            controller.ChangeLookDir(context.ReadValue<Vector2>());
        }
    }

    public void ChangeFireInput(InputAction.CallbackContext context)
    {
        if (controller)
        {
            controller.ChangeFireInput(context.ReadValue<float>());
        }
    }
}
