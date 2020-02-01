using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Vector2 launchSpeed;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemies>().DealDamage(10);
        }
        Destroy(gameObject);
    }
}
