using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticArea : MonoBehaviour
{
    private List<Rigidbody2D> held_rigids;
    public float magnetic_strength = 10.0f;
    public GameObject broken_object;
    private BrokenObject broken_script;

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
        if(broken_script)
        {
            if (broken_script.broken == false)
            {
                broken_script = null;
                broken_object = null;
            }
        }
        for(int i = 0; i < held_rigids.Count; i++)
        {
            if(held_rigids[i]!= null)
            {
                if (held_rigids[i].velocity.magnitude > 20.0f)
                {
                    return;
                }
                if (broken_object)
                {
                    held_rigids[i].AddForce((broken_object.transform.position - held_rigids[i].transform.position).normalized * magnetic_strength);
                }
                else
                {
                    held_rigids[i].AddForce((transform.position - held_rigids[i].transform.position).normalized * magnetic_strength);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        for(int i = 0; i < held_rigids.Count; i++)
        {
            if(held_rigids[i] == collision.attachedRigidbody)
            {
                return;
            }
        }
        if(collision.tag == "Fixable")
        {
            if(broken_object == null && collision.gameObject.GetComponent<BrokenObject>().broken == true)
            {
                broken_object = collision.gameObject;
                broken_script = broken_object.GetComponent<BrokenObject>();
            }
        }
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
        if(other.tag == "Fixable")
        {
            broken_object = null;
            broken_script = null;
        }
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
        if(held_rigids == null)
        {
            return;
        }
        for(int i = 0; i < held_rigids.Count; i++)
        {
            if(held_rigids[i] == null)
            {
                continue;
            }
            held_rigids[i].velocity = Vector2.zero;
            held_rigids[i].AddForce(transform.parent.parent.up * strength);
            
        }
        held_rigids = new List<Rigidbody2D>();
    }
}
