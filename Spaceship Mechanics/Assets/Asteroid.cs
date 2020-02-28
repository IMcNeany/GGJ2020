using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
   // public float timer = 180.0f;
   // private float current_timer = 0.0f;
    public float power = 100000000.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void LaunchAsteroid()
    {
        if (transform.position.x < 0)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 90, 0);
              //  (0, 0, 90);
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * power,ForceMode2D.Impulse);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(0, 0, -90, 0);
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * power, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
