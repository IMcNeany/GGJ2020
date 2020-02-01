using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour
{
    public Sprite broken_sprite;
    public Sprite fixed_sprite;
    public int debris_needed = 2;
    private int current_debris = 0;
    private bool broken;

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
        if (collision.attachedRigidbody.velocity.magnitude > 10.0f)
        {
            Debug.Log("Ouch " + collision.attachedRigidbody.velocity.magnitude);
            collision.gameObject.GetComponent<DebrisChecker>().DestroyDebris();
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
        }
    }
}
