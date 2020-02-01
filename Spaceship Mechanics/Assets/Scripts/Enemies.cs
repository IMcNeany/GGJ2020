using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemies : MonoBehaviour
{
    public float speed = 10.0f;
    public float enemy_health = 5.0f;
    public float walking_distance = 10.0f;
    //public float smooth_time = 10.0f;

    Transform the_player;
    private Rigidbody2D enemy_rigidbody;
    private Vector3 smoothVelocity = Vector3.zero;
    //private NavMeshAgent nav;

    public GameObject circle_collider;

    void Start()
    {
        enemy_rigidbody = GetComponent<Rigidbody2D>();
        the_player = GameObject.FindGameObjectWithTag("Player").transform;
        //circle_collider = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(the_player == null)
        {
            if (collision.tag == "Player")
            {
                the_player = collision.gameObject.transform;
            }
        }

        if (circle_collider)
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

        else
        {
            //do nothing
        }
    }


    void Update()
    {

    }
}
