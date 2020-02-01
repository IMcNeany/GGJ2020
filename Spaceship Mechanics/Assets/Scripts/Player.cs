using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    private Rigidbody2D body;
    public GameObject laser;
    [SerializeField] private float maxHealth = 100;
    private float health;
    public HealthBar healthbar;
    private bool alive;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        health = maxHealth;
        alive = true;
    }

    // Update is called once per frame
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
            body.AddForce(-transform.up * 100);
        }

        healthbar.fill = health/maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (body.velocity.magnitude > 10)
        {
            health -= 10;
        }
    }
}
