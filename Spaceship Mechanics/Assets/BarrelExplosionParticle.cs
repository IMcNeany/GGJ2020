using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosionParticle : MonoBehaviour
{
    public float lifetime = 0.75f;
    private float current_lifetime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current_lifetime += 1 * Time.deltaTime;
        if(current_lifetime >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().DealDamage(24.0f);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemies>().DealDamage(40.0f);
        }
    }
}
