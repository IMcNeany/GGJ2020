using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticArea : MonoBehaviour
{
    public List<GameObject> held_objects;
    public float magnetic_strength = 5.0f;

    private void OnDisable()
    {
        held_objects = new List<GameObject>();
    }

    private void Update()
    {
        for(int i = 0; i < held_objects.Count; i++)
        {
            held_objects[i].transform.position = Vector2.Lerp(held_objects[i].transform.position, transform.position, 1 / magnetic_strength);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Debris")
        {
            held_objects.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for(int i = 0; i < held_objects.Count; i++)
        {
            if(held_objects[i] == other.gameObject)
            {
                held_objects.RemoveAt(i);
            }
        }
    }

    public void Pulse(float strength)
    {
        for(int i = 0; i < held_objects.Count; i++)
        {
            held_objects[i].GetComponent<Rigidbody2D>().AddForce(transform.parent.parent.up * strength);
            
        }
        held_objects = new List<GameObject>();
    }
}
