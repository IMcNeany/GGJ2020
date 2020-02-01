using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField]
    Vector2 initalForce = new Vector2(1,1);

    // Start is called before the first frame update
    void Start()
    {
        SetInitalForce();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetInitalForce()
    {
        this.GetComponent<Rigidbody2D>().AddForce(initalForce,ForceMode2D.Impulse);
    }
}
