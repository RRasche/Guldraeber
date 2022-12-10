using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected Vector2 _moveDir;

    public void ChangeMoveDir(Vector2 moveDir) {
        this._moveDir = moveDir;
    }
}
