using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemies : MonoBehaviour
{
    public float speed = 10.0f;
    public float enemy_health = 50f;
    public float walking_distance = 10.0f;
    //public float smooth_time = 10.0f;

    protected Transform the_player;
    protected Rigidbody2D enemy_rigidbody;
    private Vector3 smoothVelocity = Vector3.zero;
    //private NavMeshAgent nav;

    public GameObject circle_collider;
    public GameObject box_collider;


    void Start()
    {
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        //the_player = GameObject.FindGameObjectWithTag("Player").transform;
        //circle_collider = GetComponent<CircleCollider2D>();
    }

    public void DealDamage(float _dam)
    {
        enemy_health -= _dam;
    }

    public virtual void Update()
    {
        if (enemy_health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
