using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected Vector2 _moveDir;
    protected Vector2 _lookDir;

    public void ChangeMoveDir(Vector2 moveDir) {
        this._moveDir = moveDir;
    }

    public void ChangeLookDir(Vector2 lookDir)
    {
        this._lookDir = lookDir;
    }
}
