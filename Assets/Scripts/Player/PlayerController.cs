using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float acceleration = 1;
    [SerializeField] private float turnAcceleration = 1;


    private Vector2 _moveDir;
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDir = this._moveDir;

        Vector2 oldVel = rigidbody2D.velocity;
        Vector2 targetVel = moveDir * speed;

        Vector2 vel = Vector2.Lerp(oldVel, targetVel, acceleration * Time.deltaTime);

        rigidbody2D.velocity = vel;
        if (moveDir.sqrMagnitude > 0) {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, turnAcceleration * Time.deltaTime);
            transform.forward = new Vector3(transform.forward.x, transform.forward.y, 0);
        }
    }

    public void ChangeMoveDir(Vector2 moveDir) {
        this._moveDir = moveDir;
    }
}
