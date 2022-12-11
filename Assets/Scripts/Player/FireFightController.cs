using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFightController : DrivingController
{

    [SerializeField] private Transform waterGun;
    [SerializeField] private float waterGunTurnSpeed = 3;
    [SerializeField] private float waterGunStrength = 3.0f;
    [SerializeField] private float waterGunRange = 5;

    [SerializeField] private string burningLayer = "Default";

    private Vector3 lastDir = Vector3.forward;
    private Vector3 temp; 

    ParticleSystem ps;

    private void Start()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
        transform.up = Vector2.up;
        ps = GetComponentInChildren<ParticleSystem>();
    }

    void FixedUpdate()
    {
        drive();
        if (this._lookDir.sqrMagnitude != 0)
        {   
            //waterGun.up = this._lookDir.normalized;
            temp = Vector3.Slerp(lastDir, this._lookDir.normalized, waterGunTurnSpeed * Time.fixedDeltaTime);
            //print(waterGun.forward);
            lastDir = new Vector3(temp.x, temp.y, 0);
            waterGun.up = lastDir;
            //lastDir = waterGun.up;
        }
        else
        {
            waterGun.up = lastDir;
        }
        if (this._firePressed > .5f)
        {
            var em = ps.emission;
            em.enabled = true;
            Vector2 pos_2D = new Vector2(transform.position.x, transform.position.y);
            RaycastHit2D [] hits = Physics2D.LinecastAll(pos_2D, pos_2D + new Vector2(waterGun.up.x, waterGun.up.y) * waterGunRange, LayerMask.GetMask(burningLayer));
            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.transform.parent != null)
                {
                    print(hit.collider.transform.parent.name);
                    Tile tile = hit.collider.gameObject.GetComponentInParent<Tile>();
                    if (tile != null)
                    {
                        tile.Extinguish_Me_a_BIT(waterGunStrength);
                    }
                }
            }
        }
        else
        {
            var em = ps.emission;
            em.enabled = false;
        }
    }
}
