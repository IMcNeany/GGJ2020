using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemies
{
    private float damage = 49.0f;
    public float timer = 3.0f;
    public GameObject explosion;
    public GameObject hit;
    public Player players_health;
    public float explode_timer_tick = 5.0f;
    private bool the_trigger;
    
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
            the_trigger = true;
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
            if (the_player)
            {
                the_trigger = true;
            }

            the_player = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            players_health.GetComponent<Player>().DealDamage(49.0f);
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

    // bellow is for explosioions
    void Hit()
    {
        if (hit)
        {
            //big explosion
            //players health gets set down
            explosion = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 1.0f);
            gameObject.SetActive(false);
        }
    }

    void Timer()
    {
        explode_timer_tick -= Time.deltaTime;

        if (explode_timer_tick < 0)
        {
            explosion = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(explosion, 1.0f);
            gameObject.SetActive(false);
          
        }
    }

    public override void Update()
    {
        if (the_trigger)
        {
            Timer();
        }
    }
}
