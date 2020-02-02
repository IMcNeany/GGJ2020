using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : PlayerEquipment
{
    public GameObject laser;
    public Transform offset;
    public override void Fire()
    {
        if (current_reload > 0)
        {
            return;
        }

        GameObject newLaser = Instantiate(laser, offset.position, transform.rotation);
        Vector2 impulse = transform.parent.up * 100;
        newLaser.GetComponent<Laser>().launchSpeed = impulse;
        this.GetComponentInParent<Rigidbody2D>().AddForce(-impulse);

        base.Fire();
    }

    public override void Update()
    {
        if (!floor)
        {
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);     //look rotation
            float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        base.Update();
    }
}
