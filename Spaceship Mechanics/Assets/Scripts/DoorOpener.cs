using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private DoorManager _doorManager;

    [SerializeField] private Sprite openDoorSprite;


    private void Awake()
    {
        _doorManager = FindObjectOfType<DoorManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().sprite = _doorManager.openedOpenerSprite;
            var myDoor = _doorManager.ReturnDoorToOpen(gameObject);

            myDoor.GetComponent<SpriteRenderer>().sprite = openDoorSprite;
            myDoor.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}