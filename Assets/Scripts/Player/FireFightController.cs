using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFightController : DrivingController
{

    [SerializeField] private Transform waterGun;
    [SerializeField] private float waterGunTurnSpeed = 3;
    [SerializeField] private float waterGunStrength = 3;
    [SerializeField] private float waterGunRange = 5;

    [SerializeField] private string burningLayer = "Default";

    private Vector2 lastDir = Vector2.up;


    void Update()
    {
        drive();
        if (this._lookDir.sqrMagnitude != 0)
        {
            //waterGun.up = this._lookDir.normalized;
            waterGun.up = Vector3.Slerp(waterGun.up, this._lookDir.normalized, waterGunTurnSpeed * Time.deltaTime);
            //print(waterGun.forward);
            waterGun.up = new Vector3(waterGun.up.x, waterGun.up.y, 0);
            lastDir = waterGun.up;
        }
        else
        {
            waterGun.up = lastDir;
        }
        if (this._firePressed > .5f)
        {
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            var em = ps.emission;
            em.enabled = true;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, waterGunRange, LayerMask.GetMask(burningLayer));
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
        else
        {
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
            var em = ps.emission;
            em.enabled = false;
        }
    }
}
