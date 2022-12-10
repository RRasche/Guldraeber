using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFightController : DrivingController
{

    [SerializeField] private Transform waterGun;
    [SerializeField] private float waterGunTurnSpeed = 3;

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
    }
}
