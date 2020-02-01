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
    [SerializeField] private float maxFuel = 100;
    private float fuel;

    [Header("Unity Stuff")]
    public GameObject laser;
    private Rigidbody2D body;
    public HealthBar healthbar;
    public HealthBar fuelBar;
    public int equipIndex;
    public List<PlayerEquipment> equipment;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        health = maxHealth;
        alive = true;
        fuel = maxFuel;
        equipment = new List<PlayerEquipment>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PlayerEquipment>())
            {
                equipment.Add(transform.GetChild(i).GetComponent<PlayerEquipment>());
            }
        }
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
            body.AddForce(Vector2.up * speed * Time.deltaTime);
            fuel -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(-Vector2.right * speed * Time.deltaTime);
            fuel -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(-Vector2.up * speed * Time.deltaTime);
            fuel -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(Vector2.right * speed * Time.deltaTime);
            fuel -= Time.deltaTime;
        }

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);     //look rotation
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButtonDown(0))
        {
            equipment[equipIndex].Fire();
        }

        if(healthbar)
        {
            healthbar.fill = health / maxHealth;
        }
        if (fuelBar)
        {
            fuelBar.fill = fuel / maxFuel;
        }

        if (Input.GetMouseButtonUp(0))
        {
            equipment[equipIndex].StopFire();
        }
        if (Input.GetMouseButton(0))
        {
            equipment[equipIndex].FireHeld();
        }
        if (Input.GetMouseButtonDown(1))
        {
            equipment[equipIndex].SecondaryFire();
        }
        if(Input.GetMouseButtonUp(1))
        {
            equipment[equipIndex].StopSecondaryFire();
        }
        if(Input.GetMouseButton(1))
        {
            equipment[equipIndex].SecondaryFireHeld();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            equipment[equipIndex].gameObject.SetActive(false);
            equipIndex++;
            if (equipIndex >= equipment.Count)
            {
                equipIndex = 0;
            }
            equipment[equipIndex].gameObject.SetActive(true);
        }

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

    public void AddEquipment(PlayerEquipment _equip)
    {
        equipment.Add(_equip);
        _equip.transform.parent = transform;
    }
}
