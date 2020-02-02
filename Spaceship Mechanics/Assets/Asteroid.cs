using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float timer = 180.0f;
    private float current_timer = 0.0f;
    public float power = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current_timer += Time.deltaTime;
        if(current_timer >= timer)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * power);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
