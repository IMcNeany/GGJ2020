using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemies
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Hit();
        }
        else
        {
            //do nothing
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform == the_player)
        {
            the_player = null;
        }
    }

    void Wander()
    {

    }

    void Chase()
    {
        //find & attack player
        //transform.LookAt(the_player);

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

    }
}
