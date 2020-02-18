﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] float maxHealth = 100;
    private float health;
    private bool alive;
    [SerializeField] private readonly float maxFuel = 100;
    private float fuel;
    [SerializeField] private readonly float maxO2 = 100;
    private float O2;

    [Header("Unity Stuff")]
    public GameObject laser;
    private Rigidbody2D body;
    public HealthBar healthbar;
    public HealthBar fuelBar;
    public HealthBar o2Bar;
    public int equipIndex;
    public GameObject jetFlames;
    public List<PlayerEquipment> equipment;
    private Light2D glowLight;
    [SerializeField] private LevelCompleter current_level;
    [SerializeField] private Transform offset;
    
    void Start()
    {
        
        body = GetComponent<Rigidbody2D>();
        health = maxHealth;
        alive = true;
        fuel = maxFuel;
        O2 = maxO2;
        equipment = new List<PlayerEquipment>();
        bool first = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<PlayerEquipment>())
            {
                AddEquipment(transform.GetChild(i).GetComponent<PlayerEquipment>());
                if (first)
                {
                    equipment[0].gameObject.SetActive(true);
                    first = false;
                }
            }
        }
        glowLight = GetComponentInChildren<Light2D>();
        jetFlames.SetActive(false);
    }

    void Update()
    {
        if(current_level == true)
        {
            if(current_level.has_oxygen == true)
            {
                O2 += Time.deltaTime * 10;
                if (O2 > maxO2)
                {
                    O2 = maxO2;
                }
            }
            else
            {
                O2 -= Time.deltaTime;
            }
        }
        else
        {
            O2 -= Time.deltaTime;
        }


        if (O2 <= 0)
        {
            health -= Time.deltaTime;
        }
        if (health <= 0 && alive)
        {
            Debug.Log("You are dead");
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }

        bool boost = false;
        if (fuel > 0)
        {
            if (Input.GetKey(KeyCode.W))       //booster input
            {
                jetFlames.SetActive(true);
                body.AddForce(Vector2.up * speed * Time.deltaTime);
                fuel -= Time.deltaTime;
                boost = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                jetFlames.SetActive(true);
                body.AddForce(-Vector2.right * speed * Time.deltaTime);
                fuel -= Time.deltaTime;
                boost = true;
            }
            if (Input.GetKey(KeyCode.S))
            {
                jetFlames.SetActive(true);
                body.AddForce(-Vector2.up * speed * Time.deltaTime);
                fuel -= Time.deltaTime;
                boost = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                jetFlames.SetActive(true);
                body.AddForce(Vector2.right * speed * Time.deltaTime);
                fuel -= Time.deltaTime;
                boost = true;
            }
        }
        if (!boost)
        {
            if(jetFlames.activeSelf)
            {
                jetFlames.SetActive(false);
            }
            fuel += Time.deltaTime * 0.5f;
        }

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);     //look rotation
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetMouseButtonDown(0))
        {
            equipment[equipIndex].Fire();
        }

        if(healthbar)       //bars
        {
            healthbar.fill = health / maxHealth;
        }
        if (fuelBar)
        {
            fuelBar.fill = fuel / maxFuel;
        }
        if (o2Bar)
        {
            o2Bar.fill = O2 / maxO2;
        }

        if (O2 / maxO2 <= 0.5 && O2 / maxO2 > 0.1)      //dim light
        {
            glowLight.intensity = (O2 * 2) / maxO2;
        }

        if (Input.GetMouseButtonUp(0))      //mouse control
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

        if (Input.GetKeyDown(KeyCode.Space))        //equipment shopping
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Level")
        {
            current_level = collision.GetComponent<LevelCompleter>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<LevelCompleter>() == current_level)
        {
            current_level = null;
        }
    }

    public void AddEquipment(PlayerEquipment _equip)
    {
        equipment.Add(_equip);
        _equip.transform.parent = transform;
        _equip.transform.rotation = transform.rotation;
        _equip.transform.localPosition = offset.localPosition;
        _equip.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(_equip.GetComponent<Rigidbody2D>());
        _equip.gameObject.SetActive(false);
    }
      
    public void DealDamage(float _dam)
    {
        health -= _dam;
    }

    public void GainHealth(float _Gain)
    {
        health += _Gain;
    }

    public void GainFuel(float _Gain)
    {
        fuel += _Gain;
    }
}

