﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : Enemies
{
    public float creeper_health = 20;
    private float damage = 49.0f;
    public float explode_timer_tick = 5.0f;
    private float currentTime = 0.0f;
    private float time = 5.0f;
    private float min_scale = 1.0f;
    private float scale = 0.50f;

    public GameObject explosion;
    public GameObject hit;
    public GameObject players_health;

    private bool the_trigger;
    
    void Start()
    {
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        players_health = GameObject.FindWithTag("Player");
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

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Hit();
        }

        base.OnCollisionEnter2D(collision);
    }

    void Chase()
    {
        //find & chase the player

        float distance = Vector3.Distance(transform.position, the_player.position);

        if (distance < walking_distance)
        {
            Vector3 moveForce = the_player.transform.position - transform.position;
            moveForce.Normalize();
            enemy_rigidbody.AddForce(moveForce * speed);
        }
    }

    // bellow is for explosioions
    void Hit()
    {
        if (hit)
        {
            //big explosion
            //players health gets set down
            players_health.GetComponent<Player>().DealDamage(24.0f);
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
            Destroy(explosion, 0.75f);
            gameObject.SetActive(false);
          
        }
    }

    public override void Update()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 destinationScale = new Vector3(1.1f, 1.1f, 1.1f);

        if (the_trigger)
        {
            //transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);

            min_scale = Time.time;
            transform.localScale = new Vector3(Mathf.PingPong(min_scale, + scale) + 1, Mathf.PingPong(min_scale, + scale) + 1, Mathf.PingPong(min_scale, + scale) + 1);

            Timer();
        }

        base.Update();
    }
}
