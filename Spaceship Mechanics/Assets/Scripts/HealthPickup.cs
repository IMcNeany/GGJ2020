﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public float health_gain = 15.0f;
    private Rigidbody2D rb;
    public GameObject the_Player;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        the_Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            the_Player.GetComponent<Player>().GainHealth(health_gain);
            Destroy(this.gameObject);
        }
    }
}
