using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemies
{

    public GameObject explosion;
    public GameObject hit;

    void Start()
    {
        enemy_rigidbody = GetComponent<Rigidbody2D>();
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (the_player == null)
        { 
            if (collision.tag == "Player")
            {
                the_player = collision.gameObject.transform;
            }
        }

        if (the_player)
        {
            Chase();
        }
 
        else
        {
            //do nothing
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform == the_player)
        {
            the_player = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Hit();
        }
    }

    void Chase()
    {
        //find & attack player

        float distance = Vector3.Distance(transform.position, the_player.position);
        /*float step = speed * Time.deltaTime;*/ // calculates distance to move

        if (distance < walking_distance)
        {
            Vector3 moveForce = the_player.transform.position - transform.position;
            moveForce.Normalize();
            enemy_rigidbody.AddForce(moveForce * speed);

            //transform.position = Vector3.MoveTowards(transform.position, the_player.position, step);
            //transform.position = Vector3.SmoothDamp(transform.position, the_player.position, ref smoothVelocity, smooth_time);
        }
    }

    void Hit()
    {
        if (hit)
        {
            //big explosion
            //players health gets set down
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
