using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponUI : MonoBehaviour
{
    public GameObject laserImage;
    public GameObject weapon1;
    public GameObject weapon2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaserPickedUp()
    {
        laserImage.SetActive(true);
    }

    public void SwitchWeapon()
    {
        if (laserImage.activeSelf)
        {
            if (weapon1.activeSelf)
            {
                weapon1.SetActive(false);
                weapon2.SetActive(true);
            }
            else
            {
                weapon1.SetActive(true);
                weapon2.SetActive(false);
            }
        }
    }
}
