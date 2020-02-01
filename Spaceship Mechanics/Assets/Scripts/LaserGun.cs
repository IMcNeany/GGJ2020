using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : PlayerEquipment
{
    public GameObject laser;
    public override void Fire()
    {
        if (current_reload > 0)
        {
            return;
        }

        GameObject newLaser = Instantiate(laser, transform.parent.position + transform.parent.up * 0.45f, transform.parent.rotation);
        Vector2 impulse = transform.parent.up * 100;
        newLaser.GetComponent<Laser>().launchSpeed = impulse;
        this.GetComponentInParent<Rigidbody2D>().AddForce(-impulse);

        base.Fire();
    }
}
