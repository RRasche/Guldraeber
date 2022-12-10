using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : PlayerController
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float climbSpeed = 1;
    [SerializeField] private float turnAcceleration = 6;

    [SerializeField] private float flyingHeight = 3;

    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        transform.position += Vector3.back * flyingHeight;
        rigidbody2D.velocity = Vector3.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDir = this._moveDir;

        flyingHeight += moveDir.y * Time.deltaTime * speed;
        transform.position += Vector3.forward * moveDir.y * Time.deltaTime * climbSpeed;


        Vector2 vel = rotate(rigidbody2D.velocity, -moveDir.x * turnAcceleration * Time.deltaTime);

        rigidbody2D.velocity = vel.normalized * speed;
        if (moveDir.sqrMagnitude > 0)
        {
            transform.forward = rigidbody2D.velocity;
            //transform.right += Vector3.forward * moveDir.x * .3f;
            transform.localEulerAngles = new Vector3(moveDir.y * 20, transform.localEulerAngles.y, -moveDir.x * 30);
        }
    }

    //https://forum.unity.com/threads/whats-the-best-way-to-rotate-a-vector2-in-unity.729605/
    Vector2 rotate(Vector2 vec, float angle)
    {
        return new Vector2(
            vec.x * Mathf.Cos(angle) - vec.y * Mathf.Sin(angle),
            vec.x * Mathf.Sin(angle) + vec.y * Mathf.Cos(angle)
        );
    }

}
