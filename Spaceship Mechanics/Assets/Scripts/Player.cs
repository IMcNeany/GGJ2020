using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody2D body;
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            body.AddForce(transform.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            body.AddForce(-transform.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            body.AddForce(-transform.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            body.AddForce(transform.right * speed * Time.deltaTime);
        }

        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newLaser = Instantiate(laser, transform.position + transform.up * 0.45f, this.transform.rotation);
            body.AddForce(-transform.up * 100);
        }
    }
}
