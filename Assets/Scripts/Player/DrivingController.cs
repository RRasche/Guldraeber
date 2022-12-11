using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingController : PlayerController
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float acceleration = 5;
    [SerializeField] private float turnAcceleration = 6;


    protected Rigidbody2D rb2D;
    private Vector2 temp;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        transform.up = Vector2.up;
    }

    protected void drive() {
        drive(1);
    }


    protected void drive(float speedMultiplier)
    {
        Vector2 moveDir = this._moveDir;

        Vector2 oldVel = rb2D.velocity;
        Vector2 targetVel = moveDir * speed * speedMultiplier;

        Vector2 vel = Vector2.Lerp(oldVel, targetVel, acceleration * Time.deltaTime);

        rb2D.velocity = vel;
        if (moveDir.sqrMagnitude > 0)
        {
            temp = Vector3.Slerp(transform.up, moveDir, turnAcceleration * Time.deltaTime);
            transform.up = new Vector3(temp.x, temp.y, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        drive();
    }
}
