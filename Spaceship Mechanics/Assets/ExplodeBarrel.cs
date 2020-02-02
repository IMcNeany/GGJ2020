using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBarrel : MonoBehaviour
{
    private Rigidbody2D RB;
    // Start is called before the first frame update
    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(RB.velocity.magnitude >= 7.0f)
        {
            Explode();
        }
        if(collision.rigidbody.velocity.magnitude >= 7.0f)
        {
            Explode();
        }
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().DealDamage(24.0f);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemies>().DealDamage(24.0f);
        }
    }

    public void Explode()
    {
        gameObject.SetActive(false);
    }
}
