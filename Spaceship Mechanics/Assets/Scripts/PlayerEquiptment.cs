using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquiptment : MonoBehaviour
{
    public float reload_time;
    protected float current_reload = 0.0f;


    private void Update()
    {
        if(current_reload < reload_time)
        {
            current_reload += 1 * Time.deltaTime;
        }
    }

    virtual public void Fire()
    {

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
