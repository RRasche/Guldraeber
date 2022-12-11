using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : PlayerController
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float climbSpeed = 1;
    [SerializeField] private float turnAcceleration = 6;
    [SerializeField] private float acceleration = 1;
    [SerializeField] private float extinguishStrength = 4.0f * 3.0f;
    [SerializeField] private float tiltSpeed = 2;


    [SerializeField] private float flyingHeight = 3;

    [SerializeField] private float minFlyingHeight = 0;
    [SerializeField] private float maxFlyingHeight = 4;

    ParticleSystem ps;

    private Rigidbody2D rigidbody2D;
    private float climbDir = 0;
    private float tiltDir = 0;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        transform.position += Vector3.back * flyingHeight;
        rigidbody2D.velocity = Vector3.right * speed;
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveDir = this._moveDir;

        if (moveDir.y > 0 && flyingHeight >= maxFlyingHeight || moveDir.y < 0 && flyingHeight <= minFlyingHeight)
        {
            moveDir.y = 0;
        }

        flyingHeight += moveDir.y * Time.fixedDeltaTime * speed;
        transform.position += Vector3.forward * moveDir.y * Time.fixedDeltaTime * climbSpeed;


        float lastVel = rigidbody2D.velocity.magnitude;
        Vector2 vel = rotate(rigidbody2D.velocity, -moveDir.x * turnAcceleration * Time.fixedDeltaTime);

        rigidbody2D.velocity = vel.normalized * Mathf.Lerp(lastVel, speed * (1 - moveDir.x * moveDir.x * .5f), acceleration*Time.fixedDeltaTime);

        climbDir = Mathf.Lerp(climbDir, moveDir.y, turnAcceleration * Time.deltaTime);
        tiltDir = Mathf.Lerp(tiltDir, -moveDir.x, turnAcceleration * Time.deltaTime);

        transform.forward = rigidbody2D.velocity;
        //transform.right += Vector3.forward * moveDir.x * .3f;
        transform.localEulerAngles = new Vector3(climbDir * 20, transform.localEulerAngles.y, tiltDir * 30);
     
        
        if (this._firePressed > 0.5)
        {   
            var em = ps.emission;
            em.enabled = true;
            Vector2 pos_2D = new Vector2(transform.position.x, transform.position.y);
            //MapGenerator.GetTileAtPosition(transform.position).Extinguish_Me_a_BIT(extinguishStrength);
            RaycastHit2D hit = Physics2D.Raycast(pos_2D, pos_2D + Vector2.up * 0.1f, Physics2D.DefaultRaycastLayers); 
            if (hit.collider != null && hit.collider.transform.parent != null)
            {
                Tile tile = hit.collider.gameObject.GetComponentInParent<Tile>();
                if(tile != null)
                {
                    tile.Extinguish_Me_a_BIT(extinguishStrength);
                }
            }
            
        }
        else
        {
            var em = ps.emission;
            em.enabled = false;
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