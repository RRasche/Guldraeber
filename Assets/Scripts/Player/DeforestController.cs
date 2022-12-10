using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeforestController : DrivingController
{
    [SerializeField] private string forestLayer = "Default";
    [SerializeField] private float deforesterDist = 0.5f;
    [SerializeField] private float demolishStrength = 1;
    [SerializeField] private float demolishSpeedMultiplier = 0.3f;

    private Vector2 lastDir = Vector2.up;


    void Update()
    {
        float speedMultiplier = this._firePressed > 0.5f ? demolishSpeedMultiplier : 1;

        drive(speedMultiplier);
        /*if (this._lookDir.sqrMagnitude != 0)
        {
            /*if (this._firePressed > .5f)
            {
                ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
                var em = ps.emission;
                em.enabled = true;

                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, , LayerMask.GetMask(burningLayer));
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
        } else
        {
            //waterGun.up = lastDir;
        }*/

        if (this._firePressed > 0.5f) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 1.1f, LayerMask.GetMask(forestLayer));

            if (hit.collider != null && hit.collider.transform.parent != null) {
                Tile tile = hit.collider.gameObject.GetComponentInParent<Tile>();
                if (tile != null) {
                    tile.Demolish_Me_a_BIT(demolishStrength);
                }
            }
        }
    }

}
