﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaper : Enemies
{
    public float jumpForce;
    private bool cling;
    [Header("Wall location")]
    [SerializeField] private bool vert;
    [SerializeField] private bool left;
    [SerializeField] private bool down;
    private Rigidbody2D body;
    [SerializeField] private float jumpTimer;
    private bool playerInRange;
    private Vector2 wallDir;

    void Start()
    {
        cling = true;
        body = GetComponent<Rigidbody2D>();
        jumpTimer = 0;

        if (!vert)
        {
            body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            if (left)
            {
                transform.rotation = Quaternion.AngleAxis(-90, Vector3.forward);
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            }
        }
        else
        {
            body.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            if (!down)
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.forward);
            }
        }
    }

    override public void Update()
    {
        jumpTimer += Time.deltaTime * Random.value;
        if (cling)
        {
            body.AddForce(wallDir);
        }
        if(jumpTimer > 5 && cling)
        {
            if (playerInRange)
            {
                Leap();
                Debug.Log("Triggered Jump");
            }
            else if (jumpTimer > 20)
            {
                Leap();
                body.AddForce(transform.up * jumpForce);
            }
        }
        if (body.velocity.magnitude < 5 && cling == true)
        {
            float n = speed;
            if (vert)
            {
                if (Random.Range(0, 2) == 0)
                {
                    n *= -1;
                }
                else
                {
                    n *= 1;
                }
                body.AddForce(transform.parent.right * n);
            }
            if (!vert)
            {
                if (Random.Range(0, 2) == 0)
                {
                    n *= -1;
                }
                else
                {
                    n *= 1;
                }
                body.AddForce(transform.parent.up * n);
            }
        }
        base.Update();
    }

    private void Leap()
    {
        body.constraints = RigidbodyConstraints2D.None;
        cling = false;
        body.velocity = new Vector2(0, 0);
        body.AddForce(transform.up * jumpForce);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D point = collision.GetContact(0);
        float dir = Mathf.Atan2(point.normal.y, point.normal.x) * Mathf.Rad2Deg;
        dir -= 90;
        wallDir = -point.normal;
        if (collision.gameObject.CompareTag("Wall"))
        {
            body.velocity = Vector2.zero;
            if (!cling)
            {
                jumpTimer = 0;
            }
            
            if (point.normal.y > 0.3)
            {
                cling = true;
                body.constraints = RigidbodyConstraints2D.FreezeRotation;
                vert = true;
                down = false;
                transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);
            }
            else if (point.normal.y < -0.3)
            {
                cling = true;
                body.constraints = RigidbodyConstraints2D.FreezeRotation;
                vert = true;
                down = true;
                transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);
            }
            else if (point.normal.x > 0.3)
            {
                cling = true;
                body.constraints = RigidbodyConstraints2D.FreezeRotation;
                vert = false;
                left = true;
                transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);
            }
            else if (point.normal.x < -0.3)
            {
                cling = true;
                body.constraints = RigidbodyConstraints2D.FreezeRotation;
                vert = false;
                left = false;
                transform.rotation = Quaternion.AngleAxis(dir, Vector3.forward);
            }
        }
        
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().DealDamage(10);
        }

        base.OnCollisionEnter2D(collision);

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Wall"))
        //{
        //    Leap();
        //}
    }

    public void InRange(bool _b)
    {
        playerInRange = _b;
    }
}
