using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingController : PlayerController
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float acceleration = 5;
    [SerializeField] private float turnAcceleration = 6;


    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        transform.up = Vector2.up;
    }

    protected void drive() {
        drive(1);
    }


    protected void drive(float speedMultiplier)
    {
        Vector2 moveDir = this._moveDir;

        Vector2 oldVel = rigidbody2D.velocity;
        Vector2 targetVel = moveDir * speed * speedMultiplier;

        Vector2 vel = Vector2.Lerp(oldVel, targetVel, acceleration * Time.deltaTime);
        rigidbody2D.velocity = vel;
        if (moveDir.sqrMagnitude > 0)
        {
            transform.up = Vector3.Slerp(transform.up, moveDir, turnAcceleration * Time.deltaTime);
            transform.up = new Vector3(transform.up.x, transform.up.y, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        drive();
    }
}
