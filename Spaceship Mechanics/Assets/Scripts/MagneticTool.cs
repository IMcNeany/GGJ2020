using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticTool : PlayerEquipment
{
    public GameObject magnetic_area;
    public GameObject charge_area;
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

        base.Fire();
    }


    public override void StopFire()
    {
        magnetic_area.SetActive(false);
    }

    public override void StopSecondaryFire()
    {
        charge_area.SetActive(false);
        Vector2 current_position = transform.position;
        charge_area.transform.localPosition = start_area;
        magnetic_area.GetComponent<MagneticArea>().Pulse(current_charge * 10.0f);
        magnetic_area.SetActive(false);
        current_reload = 0.0f;
        current_charge = 0.0f;
    }

    public override void SecondaryFireHeld()
    {
        if (current_reload < reload_time)
        {
            return;
        }
        charge_area.SetActive(true);
        current_charge += charge_speed * Time.deltaTime;
        Vector2 current_position = transform.position;
        charge_area.transform.localPosition = Vector2.Lerp(start_area, end_area, current_charge / 10.0f);
        if(current_charge >= max_charge)
        {
            current_charge = max_charge;
            StopSecondaryFire();
        }
    }

}
