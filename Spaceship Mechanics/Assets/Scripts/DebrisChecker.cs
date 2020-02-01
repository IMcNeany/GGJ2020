using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisChecker : MonoBehaviour
{
    public bool magnetized = false;
    public MagneticArea magnetic_area;

    public void DestroyDebris()
    {
        if(magnetic_area)
        {
            magnetic_area.RemoveDebris(gameObject);
        }
        Destroy(gameObject); // maybe particle?
    }
}
