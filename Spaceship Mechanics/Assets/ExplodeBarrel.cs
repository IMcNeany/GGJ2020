using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBarrel : MonoBehaviour
{
    private Rigidbody2D RB;
    public GameObject explosion;
    // Start is called before the first frame update
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(RB.velocity.magnitude >= 4.0f)
        {
            Explode();
        }
        if(collision.rigidbody.velocity.magnitude >= 5.0f)
        {
            Explode();
        }
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}
