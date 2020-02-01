using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public float reload_time;
    protected float current_reload = 0.0f;

    private void Awake()
    {
        current_reload = reload_time;
    }

    private void Update()
    {
        if(current_reload > 0)
        {
            current_reload -= Time.deltaTime;
        }
    }

    virtual public void Fire()
    {
        current_reload = reload_time;
    }

    virtual public void SecondaryFire()
    {

    }

    virtual public void StopFire()
    {

    }

    virtual public void StopSecondaryFire()
    {

    }

    virtual public void FireHeld()
    {

    }

    virtual public void SecondaryFireHeld()
    {

    }
}
