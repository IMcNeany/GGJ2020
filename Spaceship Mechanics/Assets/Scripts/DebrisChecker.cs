using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisChecker : MonoBehaviour
{
    public bool magnetized = false;
    public MagneticArea magnetic_area;
    public AudioSource audio;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle.normalized * 7.5f);
        audio = GetComponent<AudioSource>();
    }
    public void DestroyDebris()
    {
        if(magnetic_area)
        {
            magnetic_area.RemoveDebris(gameObject);
        }
        Destroy(gameObject); // maybe particle?
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(GetComponent<Rigidbody2D>().velocity.magnitude > 3.0f)
        {
            audio.pitch = Random.Range(0.3f, 0.7f);
            audio.PlayOneShot(audio.clip);
        }
    }
}
