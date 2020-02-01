using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour
{
    public Sprite broken_sprite;
    public Sprite fixed_sprite;
    public int debris_needed = 2;
    private int current_debris = 0;
    public bool broken = true;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = broken_sprite;
        broken = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (current_debris >= debris_needed)
        {
            return;
        }
        else
        {
            if (collision.gameObject.tag == "Debris")
            {
                if(collision.gameObject.GetComponent<DebrisChecker>().magnetized == true)
                {
                    current_debris++;
                    collision.gameObject.GetComponent<DebrisChecker>().DestroyDebris();
                }
            }
        }
    }

    private void Update()
    {
        if(current_debris >= debris_needed)
        {
            GetComponent<SpriteRenderer>().sprite = fixed_sprite;
            broken = false;
        }
    }
}
