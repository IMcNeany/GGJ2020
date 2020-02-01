using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class MagneticTool : PlayerEquipment
{
    public GameObject magnetic_area;
    public Light2D charge_light;
    public Vector2 start_area;
    public Vector2 end_area;
    public float max_charge = 10.0f;
    public float charge_speed = 2.5f;
    private float current_charge = 0.0f;


    public override void Fire()
    {
        if(current_reload > 0)
        {
            return;
        }
        magnetic_area.SetActive(true);
    }


    public override void StopFire()
    {
        magnetic_area.SetActive(false);
    }

    public override void StopSecondaryFire()
    {
        charge_light.pointLightOuterRadius = 0.0f;
        Vector2 current_position = transform.position;
        magnetic_area.GetComponent<MagneticArea>().Pulse(current_charge * 10.0f);
        transform.parent.GetComponent<Rigidbody2D>().AddForce(transform.up * -current_charge * 10.0f);
        magnetic_area.SetActive(false);
        current_reload = reload_time;
        current_charge = 0.0f;
    }

    public override void SecondaryFireHeld()
    {
        if (current_reload > 0)
        {
            return;
        }
        current_charge += charge_speed * Time.deltaTime;
        Vector2 current_position = transform.position;

        charge_light.pointLightOuterRadius = current_charge / 4;
        if(current_charge >= max_charge)
        {
            current_charge = max_charge;
            StopSecondaryFire();
        }
    }

}
