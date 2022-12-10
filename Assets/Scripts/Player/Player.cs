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

    public void ChangeMoveInput(InputAction.CallbackContext context) {
        controller.ChangeMoveDir(context.ReadValue<Vector2>());
    }
}
