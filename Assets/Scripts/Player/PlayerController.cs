using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected Vector2 _moveDir;
    protected Vector2 _lookDir;
    protected float _firePressed;
    public void ChangeMoveDir(Vector2 moveDir) {
        this._moveDir = moveDir;
    }

    public void ChangeLookDir(Vector2 lookDir)
    {
        this._lookDir = lookDir;
    }

    public void ChangeFireInput(float fireInput)
    {
        this._firePressed = fireInput;
    }
}
