using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float maxHealth = 100;
    private float health;
    private bool alive;

    [Header("Unity Stuff")]
    public GameObject laser;
    private Rigidbody2D body;
    public HealthBar healthbar;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        health = maxHealth;
        alive = true;
    }

    void Update()
    {
        if (health <= 0 && alive)
        {
            Debug.Log("You are dead");
            Destroy(gameObject);
        }

        if (Input.GetKey(KeyCode.W))       //booster input
        {
            body.AddForce(transform.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(-transform.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(-transform.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(transform.right * speed * Time.deltaTime);
        }

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);     //look rotation
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetKeyDown(KeyCode.Space))        //laser control
        {
            GameObject newLaser = Instantiate(laser, transform.position + transform.up * 0.45f, this.transform.rotation);
            Vector2 impulse = transform.up * 100;
            newLaser.GetComponent<Laser>().launchSpeed = impulse;
            body.AddForce(-impulse);
        }

        healthbar.fill = health/maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D point = collision.GetContact(0);
        if (point.normal.y > 0.3 || point.normal.y < -0.3)
        {
            if (body.velocity.y > 10 || body.velocity.y < -10)
            {
                health -= 10;
            }
        }
        if (point.normal.x > 0.3 || point.normal.x < -0.3)
        {
            if (body.velocity.x > 10 || body.velocity.x < -10)
            {
                health -= 10;
            }
        }
    }
}
