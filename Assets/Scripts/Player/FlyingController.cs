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

    [SerializeField] private GameObject bucket;

    [SerializeField] private float wateringRate = .2f;
    [SerializeField] private float fillingRate = 2.0f;
    private float waterAmount = 1;


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

        bucket.transform.localScale = Vector3.one * Mathf.Lerp(.3f, 2, waterAmount);

        float lastVel = rigidbody2D.velocity.magnitude;
        Vector2 vel = rotate(rigidbody2D.velocity, -moveDir.x * turnAcceleration * Time.fixedDeltaTime);

        rigidbody2D.velocity = vel.normalized * Mathf.Lerp(lastVel, speed * (1 - moveDir.x * moveDir.x * .5f), acceleration*Time.fixedDeltaTime);

        climbDir = Mathf.Lerp(climbDir, moveDir.y, turnAcceleration * Time.deltaTime);
        tiltDir = Mathf.Lerp(tiltDir, -moveDir.x, turnAcceleration * Time.deltaTime);

        transform.forward = rigidbody2D.velocity;
        //transform.right += Vector3.forward * moveDir.x * .3f;
        transform.localEulerAngles = new Vector3(climbDir * 20, transform.localEulerAngles.y, tiltDir * 30);


        //Fill water
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (Vector2)transform.position + Vector2.up * 0.1f, LayerMask.GetMask("WaterTile"));
        if (hit.collider != null)
        {
            Tile tile = hit.collider.GetComponentInParent<Tile>();
            if (tile.type == Tile.TileType.WATER)
            {
                // OVER WATER TILE
                waterAmount += fillingRate * Time.fixedDeltaTime;
                waterAmount = Mathf.Clamp(waterAmount, 0, 1);
            }
        }

        // Fire water
        if (this._firePressed > 0.5 && waterAmount > 0)
        {
            waterAmount -= wateringRate * Time.fixedDeltaTime;
            var em = ps.emission;
            em.enabled = true;
            Vector2 pos_2D = new Vector2(transform.position.x, transform.position.y);
            //MapGenerator.GetTileAtPosition(transform.position).Extinguish_Me_a_BIT(extinguishStrength);
            hit = Physics2D.Raycast(pos_2D, pos_2D + Vector2.up * 0.1f, Physics2D.DefaultRaycastLayers); 
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