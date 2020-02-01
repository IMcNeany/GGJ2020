using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticArea : MonoBehaviour
{
    private List<Rigidbody2D> held_rigids;
    public float magnetic_strength = 5.0f;

    private void Awake()
    {
        if (held_rigids == null)
        {
            held_rigids = new List<Rigidbody2D>();
        }
    }
    private void OnDisable()
    {
        held_rigids = new List<Rigidbody2D>();
    }
    public void RemoveDebris(GameObject obj)
    {
        for(int i = 0; i < held_rigids.Count; i++)
        {
            if(held_rigids[i].gameObject == obj)
            {
                held_rigids.RemoveAt(i);
                return;
            }
        }
    }

    private void Update()
    {
        for(int i = 0; i < held_rigids.Count; i++)
        {
            if(held_rigids[i].velocity.magnitude > 20.0f)
            {
                return;
            }
            held_rigids[i].AddForce((transform.position - held_rigids[i].transform.position).normalized * magnetic_strength);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Debris")
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                held_rigids.Add(collision.gameObject.GetComponent<Rigidbody2D>());
                collision.gameObject.GetComponent<DebrisChecker>().magnetized = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        for(int i = 0; i < held_rigids.Count; i++)
        {
            if(held_rigids[i] == other.gameObject.GetComponent<Rigidbody2D>())
            {
                held_rigids[i].gameObject.GetComponent<DebrisChecker>().magnetized = false;
                held_rigids.RemoveAt(i);
            }
        }
    }

    public void Pulse(float strength)
    {
        for(int i = 0; i < held_rigids.Count; i++)
        {
            held_rigids[i].AddForce(transform.parent.parent.up * strength);
            
        }
        held_rigids = new List<Rigidbody2D>();
    }
}
