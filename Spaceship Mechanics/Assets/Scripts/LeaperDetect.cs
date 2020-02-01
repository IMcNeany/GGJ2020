using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaperDetect : MonoBehaviour
{
    private Leaper parent;
    private void Start()
    {
        parent = transform.parent.GetComponent<Leaper>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            parent.InRange(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            parent.InRange(false);
        }
    }
}
