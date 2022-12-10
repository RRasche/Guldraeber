using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector2 _moveDir;

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDir = this._moveDir;
        //Debug.Log(moveDir);
        transform.position += new Vector3(moveDir.x, moveDir.y, 0);
        if (moveDir.sqrMagnitude > 0) {
            transform.forward = new Vector3(moveDir.x, moveDir.y, 0).normalized;
        }
    }

    public void ChangeMoveDir(Vector2 moveDir) {
        this._moveDir = moveDir * Time.deltaTime * this.speed;
    }
}
